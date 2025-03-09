-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\sum2.vhd
-- Generated   : Fri Oct 16 13:41:20 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\sum2.bde
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

entity sum2 is
  port(
       P1 : in STD_LOGIC;
       A0 : in STD_LOGIC;
       B0 : in STD_LOGIC;
       A1 : in STD_LOGIC;
       B1 : in STD_LOGIC;
       S0 : out STD_LOGIC;
       S1 : out STD_LOGIC;
       P0 : out STD_LOGIC
  );
end sum2;

architecture sum2 of sum2 is

---- Component declarations -----

component sum1
  port(
       P1 : in STD_LOGIC;
       A : in STD_LOGIC;
       B : in STD_LOGIC;
       S : out STD_LOGIC;
       P0 : out STD_LOGIC
  );
end component;

---- Signal declarations used on the diagram ----

signal NET669 : STD_LOGIC;

begin

----  Component instantiations  ----

U1 : sum1
  port map(
       P1 => P1,
       A => A0,
       B => B0,
       S => S0,
       P0 => NET669
  );

U2 : sum1
  port map(
       P1 => NET669,
       A => A1,
       B => B1,
       S => S1,
       P0 => P0
  );


end sum2;
