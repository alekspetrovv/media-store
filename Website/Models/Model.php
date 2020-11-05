<?php

require 'Models/DB.php';

class Model extends DB {

    protected $table = null;
    protected $params = [];

    private $start_query;
    private $select = "*";
    private $query;

    private $bindings;
    private $where;
    private $set;

    private $orderBy;
    private $orderByType;


    function __construct() {
        $this->start_query = "SELECT " . $this->select . " FROM " . $this->table; //SELECT * FROM schedules
        $this->where = [];
        $this->bindings = [];
        $this->set = [];
        $this->setClass(get_class($this));
        parent::__construct();
    }

    public function setClass($class) {
        $this->class = $class;
    }

    public function select($select) {
        $this->start_query = "SELECT " . $select . " FROM " . $this->table;
    }

    public function orderBy($field, $type = 'ASC') {
        $this->orderBy = $field;
        $this->orderByType = $type;
    }


    public function save() {

        if (empty($this->id)) {
            //create
            $this->start_query = "INSERT INTO `$this->table`";
            $this->start_query .= "(";
            foreach ($this->fillable as $key => $field) {
                if ($field == 'id') {
                    continue;
                }
                reset($this->fillable);

                $this->start_query .= "`$field`";

                end($this->fillable);
                if ($key != key($this->fillable)) {
                    $this->start_query .= ", ";
                }
            }

            $this->start_query .= ") VALUES (";

            foreach ($this->fillable as $key => $field) {
                if ($field == 'id') {
                    continue;
                }
                reset($this->fillable);

                $this->start_query .= ":$field";

                end($this->fillable);
                if ($key != key($this->fillable)) {
                    $this->start_query .= ", ";
                }
            }

            $this->start_query .= ")";

            foreach ($this->fillable as $field) {
                if ($field == 'id') {
                    continue;
                }
                if (property_exists($this, $field)) {
                    $this->bindings[$field] = $this->$field;
                }
            }

        } else {
            //update
            $this->start_query = "UPDATE " . $this->table . " SET ";
            foreach ($this->fillable as $key => $field) {
                reset($this->fillable);
                $this->start_query.= "`$field` = :$field";

                end($this->fillable);
                if ($key != key($this->fillable)) {
                    $this->start_query .= ", ";
                }
                $this->bindings[$field] = $this->$field;
            }
            $this->where('id', $this->id);
        }
//
        $this->perform();
        return $this->first();
    }

    public function format($text) {
        // replace non letter or digits by -
        $text = preg_replace('~[^\pL\d]+~u', '-', $text);

        // transliterate
        $text = iconv('utf-8', 'us-ascii//TRANSLIT', $text);

        // remove unwanted characters
        $text = preg_replace('~[^-\w]+~', '', $text);

        // trim
        $text = trim($text, '-');

        // remove duplicate -
        $text = preg_replace('~-+~', 'fuckmylife', $text);

        // lowercase
        $text = strtolower($text);

        if (empty($text)) {
            return 'n-a';
        }

        return $text;
    }

    public function delete() {
        $this->start_query = "DELETE FROM $this->table";

        if (!empty($this->id) && empty($this->where)) {
            $this->where('id', $this->id);
        }
        $this->perform();
        $this->execute();

        return true;
    }

    public function where($param, $condition, $value = null) {
        if (empty($value)) {
            $value = $condition;
            $condition = "=";
        }

        $this->where[] = "" .$param . " " . $condition . " :" . $this->format($param);
        $this->bindings[":" . $this->format($param)] = $value;
    }

    public function with($relations) {
        foreach ($relations as $relation ) {
           $this->$relation();
        }
        return $this;
    }

    public function hasMany($class, $local_key, $foreignKey) {
        $this->join[] = [$class, $local_key, $foreignKey];
    }

    public function perform() {
        $this->query = $this->start_query;


        if (!empty($this->join)) {
            foreach ($this->join as $relation) {

                $joined_class = new $relation[0];
                $this->query .= " INNER JOIN " . $joined_class->table . " ON " . $this->table . "." . $relation[1] . " = " . $joined_class->table . "." . $relation[2];
            }
        }

        if (!empty($this->where)) {
            $this->query .= " WHERE";

            foreach ($this->where as $key => $where_query) {
                reset($this->where);
                $this->query .= " $where_query";

                //if not the last element
                end($this->where);
                if ($key != key($this->where)) {
                    $this->query .= " AND";
                }
            }
        }

        if (!empty($this->set)) {
            $this->query .= " WHERE";

            foreach ($this->where as $key => $where_query) {
                reset($this->where);
                $this->query .= " $where_query";

                //if not the last element
                end($this->where);
                if ($key != key($this->where)) {
                    $this->query .= " AND";
                }
            }
        }

        if (!empty($this->orderBy)) {
            $this->query .= " ORDER BY " . $this->orderBy . " " . $this->orderByType;
        }

        $this->query($this->query);
        $this->bindings($this->bindings);
    }

    public function execute() {
        $this->perform();
        parent::execute();
    }

    public function first() {
        $this->perform();
        return parent::first();
    }

    public function get() {
        $this->perform();
        return parent::get();
    }

}