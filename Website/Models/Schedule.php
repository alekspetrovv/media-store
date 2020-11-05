<?php

require_once 'Models/Model.php';
require_once 'Models/ScheduleMember.php';

class Schedule extends Model {

    protected $table = 'schedules';
    public $fillable = ['notes', 'date'];

    public function members() {
        return $this->hasMany(ScheduleMember::class, 'id', 'schedule_id');
    }
}