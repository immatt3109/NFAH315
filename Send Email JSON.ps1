$file = "C:\Crestron\Projects\C# NFAH\NFAHRooms\email.json"
$remotefile = "/user/email.json"
$ip = "192.168.1.95"
$username = "admin"
$password = "password"
Send-FTPFile -device $ip -LocalFile $file -RemoteFile $remotefile -Secure -Username $username -Password $password