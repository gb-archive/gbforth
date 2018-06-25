title: SEQUENCER
gamecode: SQCR
makercode: TK

require gbhw.fs
require input.fs
require term.fs

require ./sound.fs

( program start )

VARIABLE key-released

VARIABLE selected
5 CONSTANT #notes

VARIABLE fcurr
VARIABLE current
8 CONSTANT #patterns
RAM CREATE pattern #patterns CELLS ALLOT

: current++
  current @ 1+
  #patterns mod
  current ! ;

: selected++
  selected @ 1+
  #patterns mod
  selected !
  2 9 at-xy ."                 "
  2 selected @ 2 * + 9 at-xy ." I" ;

: selected--
  selected @ 1-
  #patterns + #patterns mod
  selected !
  2 9 at-xy ."                 "
  2 selected @ 2 * + 9 at-xy ." I" ;

: pattern[selected]
  pattern selected @ cells + ;

: note++
  pattern[selected] dup
  @
  1+ #notes mod
  dup
  rot !
  2 selected @ 2 * + 8 at-xy
  dup 0 = if ."  " then
  dup 1 = if ." 1" then
  dup 2 = if ." 2" then
  dup 3 = if ." 3" then
  dup 4 = if ." 4" then
  drop ;

: note--
  pattern[selected] dup
  @
  1- #notes + #notes mod
  dup
  rot !
  2 selected @ 2 * + 8 at-xy
  dup 0 = if ."  " then
  dup 1 = if ." 1" then
  dup 2 = if ." 2" then
  dup 3 = if ." 3" then
  dup 4 = if ." 4" then
  drop ;

: handle-input
  begin
    rDIV c@ [ $FF 128 / ]L < if
      1 fcurr +!
      fcurr @ 6 > if
        0 fcurr !
        current++
        2 7 at-xy
        ."                 "
        2 current @ 2 * + 7 at-xy
        ." V"
        pattern current @ cells + @
          dup 1 = if A3-med play-note-chan2 then
          dup 2 = if C4-med play-note-chan2 then
          dup 3 = if E4-med play-note-chan2 then
          dup 4 = if G4-med play-note-chan2 then
        drop
      then

      key-state
        dup k-down  and key-released @ and if note-- then
        dup k-left  and key-released @ and if selected-- then
        dup k-up    and key-released @ and if note++ then
        dup k-right and key-released @ and if selected++ then
        \ dup k-a  and if select-notes-a then
        \ dup k-b  and if select-notes-b then
        dup 0= key-released !
      drop
    then
  again ;

: erase 0 fill ;

: main
  0 fcurr !
  0 current !
  0 selected !
  true key-released !
  pattern #patterns cells erase
  init-term
  init-input
  page
  0 0 at-xy
  handle-input ;
