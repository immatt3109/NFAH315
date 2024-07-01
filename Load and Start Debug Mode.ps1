$file = "C:\Crestron\Projects\C# NFAH\NFAHRooms\bin\Debug\NFAHRooms.cpz"
$fileLocationOnProcessor = "/program01/NFAHRooms.cpz"
$ip = "192.168.1.95"
$port = "50000"
$slot = "1"
$username = "admin"
$password = "password"


Send-CrestronProgram -device $ip -LocalFile $file -ProgramSlot $slot -DoNotStart -Secure -Username $username -Password $password -ShowProgress
Invoke-CrestronCommand -Device $ip -Command "progload -p:$slot -d" -Secure -Username $username -Password $password -Timeout 20
Invoke-CrestronCommand -Device $ip -Command "Debugprogram -p:$slot -Port:$port -ip:0.0.0.0 -s" -Secure -Username $username -Password $password -Timeout 20
Invoke-CrestronCommand -Device $ip -Command "progres -p:$slot" -Secure -Username $username -Password $password -Timeout 20
