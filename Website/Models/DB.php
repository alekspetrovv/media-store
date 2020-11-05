<?php

class DB {
    private $user;
    private $db_password;
    private $db;
    private $host;

    private $query;
    private $bindings;
    private $stmt;
    protected $class;
    protected $join;

    function __construct() {
        $this->user = 'root';
        $this->db_password = '123456';
        $this->db = 'papasenpai';
        $this->host = 'localhost';

        $this->query = null;
        $this->bindings = [];
        $this->join = [];
    }


    public function query($query) {
        $this->query = $query;
    }

    public function bindings($bindings) {
        $this->bindings = $bindings;
    }

    public function execute() {
        $stmt = $this->connect()->prepare($this->query);

        foreach ($this->bindings as $binding_key => $binding_value) {
            $stmt->bindValue($binding_key, $binding_value);
        }
        $stmt->execute();
        $this->stmt = $stmt;
    }

    public function first() {
        $this->execute();
        return $this->fetch();
    }

    public function get() {
        $this->execute();

        return $this->fetchAll();
    }

    public function fetch() {
        $result = $this->stmt->fetch();


        if (empty($result)) {
            return null;
        }

        $class = new $this->class;
        foreach ($result as $key => $value) {
            $class->$key = $value;
        }

        return $class;
    }

    public function fetchAll() {
        $results = $this->stmt->fetchAll();
        $response = array();

        if (empty($results)) {
            return null;
        }
        foreach ($results as $actual_object) {
            $class = new $this->class;
            foreach ($actual_object as $key => $value) {
                $class->$key = $value;
            }
            $response[] = $class;
        }


        return $response;
    }

    public function connect() {
        $pdoOptions = array(
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_EMULATE_PREPARES => false
        );

        /**
         * Connect to MySQL and instantiate the PDO object.
         */
        return new PDO(
            "mysql:host=" . $this->host . ";dbname=" . $this->db, //DSN
            $this->user, //Username
            $this->db_password, //Password
            $pdoOptions //Options
        );
    }
}