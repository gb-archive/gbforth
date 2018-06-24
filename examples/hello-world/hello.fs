title: EXAMPLE
gamecode: HELO
makercode: RX

require gbhw.fs
require input.fs
require term.fs

( program start )

: rSCX+! rSCX c@ + rSCX c! ;
: rSCY+! rSCY c@ + rSCY c! ;

: handle-input
  begin
    key case
      k-right of -5 rSCX+! endof
      k-left  of  5 rSCX+! endof
      k-up    of  5 rSCY+! endof
      k-down  of -5 rSCY+! endof
    endcase
  again ;

: main
  init-term
  init-input
  page
  3 7 at-xy
  ." Hello World !"
  handle-input ;
