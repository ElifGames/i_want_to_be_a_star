<?php
 
$class = $_POST['class'];
$name = $_POST['name'];
$score = $_POST['score'];

$connect = mysqli_connect("localhost","pid011","1234qwer", "pid011");
$connect->set_charset('utf8');
// INSERT INTO `game_record` (`class`, `name`, `score`) VALUES ('1505', '\다\혜', '134');
$sql = "INSERT INTO game_record (class, name, score) VALUES ('$class', '$name', '$score')";
$result = mysqli_query($connect, $sql);

if ($result == false) {
    echo mysqli_error($connect);
}
?>