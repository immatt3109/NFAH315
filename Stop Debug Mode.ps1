$ip = "192.168.1.95"
$slot = "1"
$username = "admin"
$password = "password"

Invoke-CrestronCommand -Device $ip -Command "stopprogram -p:$slot" -Secure -Username $username -Password $password -Timeout 20
Invoke-CrestronCommand -Device $ip -Command "debugprogram -p:$slot -c" -Secure -Username $username -Password $password -Timeout 20
Invoke-CrestronCommand -Device $ip -Command "progres -p:$slot" -Secure -Username $username -Password $password -Timeout 20