<?php

require 'Models/Employee.php';
/**
 * start the session.
 */
session_start();
/**
 * Display errors because in my system (linux) i can't see them.
 */
ini_set('display_errors', 1);
/**
 * Include our MySQL connection.
 */

/**
 * Create error array so we can store errors
 */
$errors = array();


//the user has already logged in
//if (!empty($_SESSION['user'])) {
//    header('Location: index.php');
//}


if (isset($_POST['login'])) {
    $username = !empty($_POST['username']) ? trim($_POST['username']) : null;
    $pass = !empty($_POST['password']) ? trim($_POST['password']) : null;


    $user = new Employee();
    $user->where('username', $username);
    $user->where('password', $pass);


    //Execute.
    $user = $user->first();


    //if a user found with the same email or password generate error
    if (empty($user)) {
        $errors[] = 'Wrong password or username. Please try again.';
    } else {
        //login the user
        $_SESSION['user'] = serialize($user);
        header('Location: schedule.php');
    }
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
    <title>Login Page</title>
</head>

<body>
    <div class="container">
        <nav class="navbar navbar-expand-lg bg-dark">
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
                        <a class="nav-link active" href="#">Schedules<span class="sr-only">(current)</span></a>
                    </li>
                    <li class="nav-item active">
                        <a class="nav-link" href="#">User Settings</a>
                    </li>

                </ul>
            </div>
        </nav>
        <div class="jumbotron jumbotron-fluid bg-success text-dark text-center mb-0">
            <h1 class="display-5">Welcome to PapaSenpai!</h1>
            <p class="lead">This website is made to improve employee working process.</p>
            <hr>
            <div class="wrapper">
                <?php
                if (!empty($errors)) {
                    foreach ($errors as $error) {
                        ?>
                        <div class="alert alert-error" role="alert">
                            Your username and password doesn't match.
                        </div>
                        <?php
                    }
                }
                ?>

                <form class="form-signin" method="POST" action="index.php">
                    <h5 class="form-signin-heading">Login with your credentials</h5>
                    <input type="text" class="form-control" name="username" placeholder="Enter Username" required=""
                        autofocus="">
                    <input type="password" class="form-control" name="password" placeholder="Enter Password"
                        required="" />
                    <input type="submit" class="btn btn-lg btn-success btn-block" name="login" value="Login">
                </form>
            </div>
        </div>
        <div class="container-fluid text-left bg-dark text-white">
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