<?php

ini_set('display_errors', 1);

require 'Models/Employee.php';
require 'Models/Schedule.php';
/**
 * start the session.
 */
session_start();
//the user has already logged in
if (empty($_SESSION['user'])) {
    header('Location: index.php');
}
$user = unserialize($_SESSION['user']);

$schedules = new Schedule();
$schedules->with(['members']);
$schedules->where('schedules_employees.employee_id', $user->id);
$schedules = $schedules->get();


?>
<!doctype html>
<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="stylesheet" type="text/css" href="style.css">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&display=swap" rel="stylesheet">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"
          integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <title>Home Page</title>
</head>

<body>
<div class="container">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <a class="navbar-brand" href="#">
            <img src="papasenpai.png" width="50" height="30" class="d-inline-block align-top" alt="" loading="lazy">
            PapaSenpai
        </a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a class="nav-link active" href="schedule.php">Schedules<span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="settings.php">User Settings</a>
                </li>
                <li class="nav-item active">
                    <a class="nav-link" href="logout.php">Log Out</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="jumbotron jumbotron-fluid bg-success text-white text-center mb-0">
        <h1 class="display-5">Schedules</h1>
        <br>
        <p class="lead">This page shows the employee schedule!</p>
        <hr>
        <table class="table table-hover table-dark">
            <thead>
            <tr>
                <th scope="col">Date</th>
                <th scope="col">Working Hours</th>
                <th scope="col">Salary</th>
            </tr>
            </thead>
            <tbody>
            <?php
            if (!empty($schedules)) {
                foreach ($schedules as $schedule) {
                    ?>
                    <tr>
                        <td><?= $schedule->date; ?></td>
                        <?php
                        $start = DateTime::createFromFormat("d-m-Y H:i", $schedule->from_hour);
                        $end = DateTime::createFromFormat("d-m-Y H:i", $schedule->to_hour);
                        $salary = $end->diff($start)->h + ($end->diff($start)->days * 24) * $user->wage_per_hour;
                        ?>
                        <td><?= $start->format("H:i"); ?> - <?= $end->format("H:i"); ?></td>
                        <td><?= $salary; ?></td>
                    </tr>
                    <?php
                }
            }
            ?>
            </tbody>
        </table>


    </div>
    <div class="container-fluid text-left bg-dark text-light">
        <div class="row">
            <div class="col-lg" mr>
                <p>Copyright © 2020:PapaSenpai</p>
            </div>
        </div>
    </div>

</div>

<!-- Optional JavaScrip -->
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"
        integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj"
        crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"
        integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN"
        crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js"
        integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s"
        crossorigin="anonymous"></script>
</body>

</html>