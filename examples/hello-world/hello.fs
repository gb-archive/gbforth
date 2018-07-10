title: EXAMPLE
gamecode: HELO
makercode: RX

require gbhw.fs
require input.fs
require term.fs
require ibm-font.fs

( program start )

: rSCX+! rSCX c@ + rSCX c! ;
: rSCY+! rSCY c@ + rSCY c! ;

: save-x $A000 c! ;
: load-x $A000 c@ ;

: save-y $A002 c! ;
: load-y $A002 c@ ;

: handle-input
  begin
    rDIV c@ [ $FF 8 / ]L < if
      key-state
      dup k-right and if -1 rSCX+! then
      dup k-left  and if  1 rSCX+! then
      dup k-up    and if  1 rSCY+! then
      dup k-down  and if -1 rSCY+! then
      dup k-select and if rSCX c@ save-x rSCY c@ save-y then
      dup k-start and if load-x rSCX c! load-y rSCY c! then
      \ If there no key pressed, wait for one
      0= if halt then
    then
  again ;

: main
  install-font
  init-term
  page
  3 7 at-xy
  ." Hello World !"
  handle-input ;
