$file = "C:\Crestron\Projects\C# NFAH\NFAHRooms\room_setup.json"
$remotefile = "/user/room_setup.json"
$ip = "192.168.1.95"
$username = "admin"
$password = "password"
Send-FTPFile -device $ip -LocalFile $file -RemoteFile $remotefile -Secure -Username $username -Password $password