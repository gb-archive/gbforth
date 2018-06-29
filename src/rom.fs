require ./utils/bytes.fs

32 kB constant rom-size
create rom-base rom-size allot
variable rom-offset-variable

: rom-offset rom-offset-variable @ ;
: rom-offset! rom-offset-variable ! ;
: rom-offset+! rom-offset-variable +! ;

: rom-buffer rom-base rom-size ;

( Initialize the room to zeros )
rom-buffer erase


: assert-rom-addr ( offset -- offset )
  dup $C000 $E000 within abort" Trying to reference RAM address"
  dup $0000 $8000 within invert abort" Trying to reference an address outside ROM" ;

\ Convert a ROM offset to a host address.
\
\ Will throw an exception if the rom offset is out of range.
: <rom ( offset -- addr )
  assert-rom-addr
  rom-base + ;

: rom@ ( offset -- val )
  <rom dup
  c@ swap
  1+ c@ 8 lshift + ;

: romc@ ( offset -- val )
  <rom c@ ;

: rom! ( val offset -- )
  <rom 2dup
  swap lower-byte swap c!
  swap higher-byte swap 1+ c! ;

: romc! ( c offset -- )
  <rom c! ;

: rom, ( val -- )
  rom-offset rom!
  $2 rom-offset+! ;

: romc, ( c -- )
  rom-offset romc!
  $1 rom-offset+! ;

: rom-move ( addr u -- )
  dup >r
  rom-offset <rom swap move
  r> rom-offset+! ;

: rom" ( -- offset u )
  rom-offset
  [char] " parse 2dup rom-move
  nip ;

: ==> ( n -- )
  rom-offset! ;

0 Value rom-fd
: dump-rom ( c-addr u -- )
  w/o bin create-file throw TO rom-fd
  rom-buffer rom-fd write-file throw
  rom-fd close-file throw ;
