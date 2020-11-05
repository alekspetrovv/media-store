<?php

ini_set('display_errors', 1);

require 'Models/Employee.php';
/**
 * start the session.
 */
session_start();
//the user has already logged in
if (empty($_SESSION['user'])) {
    header('Location: index.php');
}
$user = unserialize($_SESSION['user']);

if (isset($_POST['update']) && $_POST['current_password'] == $user->password) {
    $first_name = !empty($_POST['first_name']) ? trim($_POST['first_name']) : null;
    $last_name = !empty($_POST['last_name']) ? trim($_POST['last_name']) : null;
    $email = !empty($_POST['email']) ? trim($_POST['email']) : null;
    $phone_number = !empty($_POST['phone_number']) ? trim($_POST['phone_number']) : null;
    $city = !empty($_POST['city']) ? trim($_POST['city']) : null;
    $country = !empty($_POST['country']) ? trim($_POST['country']) : null;
    $address = !empty($_POST['address']) ? trim($_POST['address']) : null;
    $password = !empty($_POST['password']) ? trim($_POST['password']) : null;

    $user->first_name = $first_name;
    $user->last_name = $last_name;
    $user->email = $email;
    $user->phone_number = $phone_number;
    $user->city = $city;
    $user->country = $country;
    $user->address = $address;

    if (!empty($password)) {
        $user->password = $password;
    }

    //Execute.
     $user->save();
    $success[] = 'You have successfully updated your profile information!';
}


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
        <h1 class="display-5">User Settings</h1>
        <br>
        <p class="lead">This page shows the employee information!</p>
        <hr>
        <div class="col bg">
            <div class="card card-fluid bg-dark">
                <h6 class="card-header">User Account</h6>
                <?php
                if (!empty($success)) {
                    foreach ($success as $message) {
                        ?>
                        <div class="alert alert-success" role="alert">
                            <?=$message?>
                        </div>
                        <?php
                    }
                }
                ?>


                <div class="card-body">
                    <form method="post">
                        <div class="form-row">
                            <div class="col-md-6 mb-3">
                                <label for="input01">First Name</label>
                                <input name="first_name" type="text" class="form-control" id="input01"
                                       value="<?= $user->first_name ?>" required="">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="input02">Last Name</label>
                                <input name="last_name" type="text" class="form-control" id="input02"
                                       value="<?= $user->last_name ?>" required="">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-6 mb-3">
                                <label for="input03">Email</label>
                                <input name="email" type="email" class="form-control" id="input03"
                                       value="<?= $user->email ?>" required="">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="input04">Phone Number</label>
                                <input name="phone_number" type="text" class="form-control" id="input04"
                                       value="<?= $user->phone_number ?>" required="">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-6 mb-3">
                                <label for="input5">Country</label>
                                <input name="country" type="text" class="form-control" id="input05"
                                       value="<?= $user->country ?>" required="">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="input06">City</label>
                                <input name="city" type="text" class="form-control" id="input06"
                                       value="<?= $user->city ?>" required="">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="col-md-6 mb-3">
                                <label for="input07">Address</label>
                                <input name="address" type="text" class="form-control" id="input07"
                                       value="<?= $user->address ?>" required="">
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="input08">Change Password</label>
                                <input name="password" type="password" class="form-control" placeholder = "Enter new password" id="input08" value="">
                            </div>
                        </div>
                        <div class="form-actions">
                            <input name="current_password" type="password" class="form-control ml-auto mr-3"
                                   id="input06"
                                   placeholder="Enter Current Password" required="">
                            <input name="update" type="submit" class="btn btn-primary mt-5" value="Update Account">
                        </div>
                    </form>
                </div>
            </div>
        </div>
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