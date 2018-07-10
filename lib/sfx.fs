\ sound effects

: init-sfx
  $77 rNR50 c!   \ VOLUME MAX = 0x77, MIN = 0x00
  $FF rNR51 c!   \ ENABLE SOUND CHANNELS (1: 0x11 / 2: 0x22 / 3: 0x44 / 4: 0x88 / All : 0xFF)
  $80 rNR52 c! ; \ TURN SOUND ON

: play-noise
  init-sfx
  %01000101 rNR42 c!
  %01111001 rNR43 c!
  %10000000 rNR44 c! ;

: play-sweep
  init-sfx
  %10000001 rNR42 c!
  %01111001 rNR43 c!
  %10000000 rNR44 c! ;

: play-water
  init-sfx
  %10000001 rNR42 c!
  %01111011 rNR43 c!
  %10000000 rNR44 c! ;

: jump
  $15 rNR10 c!
  $96 rNR11 c!
  $73 rNR12 c!
  $BB rNR13 c!
  $85 rNR14 c! ;

: jump-alt
  $15 rNR10 c!
  $09 rNR11 c! \ 96 for volume?
  $43 rNR12 c!
  $F6 rNR13 c!
  $81 rNR14 c! ;

: jump-alt-2
  $15 rNR10 c!
  $09 rNR11 c!
  $73 rNR12 c!  \ volume!!!1
  $F6 rNR13 c!
  $81 rNR14 c! ;

: shoot
  $1D rNR10 c!
  $96 rNR11 c!
  $73 rNR12 c!
  $BB rNR13 c!
  $85 rNR14 c! ;

: gameover
  $4F rNR10 c! \ or 1E or 1D for louder sound / 2E / 3E / 4E... for more "vibe"
  $96 rNR11 c!
  $B7 rNR12 c! \ B7, C7, D7...F7 for longer sound
  $BB rNR13 c!
  $85 rNR14 c! ;
