<?php
$tablename = "game_record";

$connect = mysqli_connect("localhost","pid011","1234qwer", "pid011");
 
$sql = "SELECT * FROM game_record";
$result = mysqli_query($connect, $sql);
while ($row = mysqli_fetch_array($result)) {
    echo $row['class'];
    echo '<br>';
    echo $row['name'];
    echo '<br>';
    echo $row['score'];
    echo '<br>';
    echo '<br>';
}
?>