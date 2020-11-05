<?php

require_once 'Models/Model.php';

class Employee extends Model {

    protected $table = 'employees';
    public $fillable = ['first_name', 'last_name', 'email', 'password', 'city', 'country', 'phone_number'];

}