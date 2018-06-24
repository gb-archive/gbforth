
\ gb=2048-131072/hz
: hz-to-gb
  2048
  131072 rot /
  - ;

\ hz=131072/(2048-gb)
: gb-to-hz
  2048 swap -
  131072 swap / ;

: play-note-chan1 ( x1 x2 x3 x4 -- )
  rNR14 c!
  rNR13 c!
  rNR12 c!
  rNR11 c! ;

: sweep-add
  %1000010 \ lll m ttt
  rNR10 c! ;

: sweep-sub
  %1001011 \ lll m ttt
  rNR10 c! ;

: play-note-chan2 ( x1 x2 x3 x4 -- )
  rNR24 c!
  rNR23 c!
  rNR22 c!
  rNR21 c! ;

: make-note ( note period cut -- x1 x2 x3 x4 )
  >r >r >r
  $82
  r> r> swap >r
  $40 +
  r> dup %11111111 and swap
  %11100000000 and #8 rshift
  r> $40 and $80 + +
;

: note-test-c4-long
  %10 00 0010
  %0100 0 111
  %0001 0110
  %10 000 100 ;


: note-test-duty1
  %00000010
  %01000111
  %00010110
  %10000100 ;

: note-test-duty2
  %01000010
  %01000111
  %00010110
  %10000100 ;

: note-test-duty3
  %10000010
  %01000111
  %00010110
  %10000100 ;

: note-test-duty4
  %11000010
  %01000111
  %00010110
  %10000100 ;

: A3-med $82 $A5 %01010110 %10000011 ; \ 854
: C4-med $82 $A5 %00010110 %10000000 ; \ ok 1046
: E4-med $82 $A5 %11100101 %10000100 ; \ 1253
: G4-med $82 $A5 %01100011 %10000101 ; \ 1379

: C4-long $82 $47 $16 $84 ;
: A3-long $82 $47 $55 $83 ;
: B3-long $82 $47 $DA $83 ;
: E3-long $82 $47 $C5 $81 ;

\ E3 long:                  C4 long:
\ 82 --- 10 00 0010         82 --- 10 00 0010       dd ll llll     Duty, Length load (64-L)
\ 47 --- 0100 0 111         47 --- 0100 0 111       vvvv a ppp     Starting volume, Envelope add mode, period
\ c5 --- 1100 0101          16 --- 0001 0110        ffff ffff      Frequency LSB
\ 81 --- 10 000 001         84 --- 10 000 100       tl -- -fff     Trigger, Length enable, Frequency MSB

\ c4 cut:
\ 82 --- xxxxx
\ 47 --- xxxxx
\ 16 --- xxxxx
\ C4 --- 11 000 100    ( length enable = 1 ) >> cut

\ c5 long                   c5 short
\ 82 --- xxxxx              82 --- xxxxxx
\ A5 --- 1010 0 101         A2 --- 1010 0 010  ( period: 101 vs 010  // 5vs2)
\ 0A --- 0000 1010          0A --- xxxxxx
\ 86 --- 10 000 110         86 --- xxxxxx

: C4-cut $82 $47 $16 $C4 ;
: A3-cut $82 $47 $55 $C3 ;
: B3-cut $82 $47 $DA $C3 ;
: E3-cut $82 $47 $C5 $C1 ;

: C5-long $82 $A5 $0A $86 ;
: A4-long $82 $A5 $AC $85 ;
: B4-long $82 $A5 $ED $85 ;
: E4-long $82 $A5 $E5 $84 ;

: C5-short $82 $A2 $0A $86 ;
: A4-short $82 $A2 $AC $85 ;
: B4-short $82 $A2 $ED $85 ;
: E4-short $82 $A2 $E5 $84 ;


: enable-sound
  %11111111 rNR50 c!
  %11111111 rNR51 c! ;

: play-noise
  %01000101 rNR42 c!
  %01111001 rNR43 c!
  enable-sound
  %10000000 rNR44 c! ;

: play-sweep
	%10000001 rNR42 c!
	%01111001 rNR43 c!
	enable-sound
	%10000000 rNR44 c! ;

: play-water
	%10000001 rNR42 c!
	%01111011 rNR43 c!
	enable-sound
	%10000000 rNR44 c! ;
