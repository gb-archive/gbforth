[asm]

$150 ==>

: test
  ['] drop execute
  ['] drop execute ;

label main
ps-clear,

$11 ps-push-lit,
$22 ps-push-lit,
$33 ps-push-lit,
' test # call,

label loop
halt,
loop jr,

[endasm]