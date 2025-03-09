-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\sum1.vhd
-- Generated   : Fri Oct 16 13:36:13 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\sum1.bde
-- By          : Bde2Vhdl ver. 2.6
--
-------------------------------------------------------------------------------
--
-- Description : 
--
-------------------------------------------------------------------------------
-- Design unit header --
library IEEE;
use IEEE.std_logic_1164.all;

entity sum1 is
  port(
       P1 : in STD_LOGIC;
       A : in STD_LOGIC;
       B : in STD_LOGIC;
       S : out STD_LOGIC;
       P0 : out STD_LOGIC
  );
end sum1;

architecture sum1 of sum1 is

---- Signal declarations used on the diagram ----

signal NET749 : STD_LOGIC;
signal NET761 : STD_LOGIC;
signal NET765 : STD_LOGIC;

begin

----  Component instantiations  ----

NET749 <= B and A;

NET761 <= B and P1;

NET765 <= A and P1;

S <= B or A or P1;

P0 <= NET765 or NET761 or NET749;


end sum1;
