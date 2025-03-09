-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\sum_TestBensh.vhd
-- Generated   : Fri Oct 16 16:22:19 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\sum_TestBensh.bde
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

entity sum_TestBensh is
  port(
       P1 : in STD_LOGIC;
       A0 : in STD_LOGIC;
       B0 : in STD_LOGIC;
       A1 : in STD_LOGIC;
       B1 : in STD_LOGIC;
       Z : out STD_LOGIC
  );
end sum_TestBensh;

architecture sum_TestBensh of sum_TestBensh is

---- Component declarations -----

component sum2
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
end component;
component sum2_TestBench
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
end component;

---- Signal declarations used on the diagram ----

signal NET758 : STD_LOGIC;
signal NET766 : STD_LOGIC;
signal NET774 : STD_LOGIC;
signal NET782 : STD_LOGIC;
signal NET790 : STD_LOGIC;
signal NET798 : STD_LOGIC;
signal NET807 : STD_LOGIC;
signal NET815 : STD_LOGIC;
signal NET823 : STD_LOGIC;

begin

----  Component instantiations  ----

U1 : sum2
  port map(
       P1 => P1,
       A0 => A0,
       B0 => B0,
       A1 => A1,
       B1 => B1,
       S0 => NET758,
       S1 => NET774,
       P0 => NET790
  );

Z <= NET823 or NET815 or NET807;

U2 : sum2_TestBench
  port map(
       P1 => P1,
       A0 => A0,
       B0 => B0,
       A1 => A1,
       B1 => B1,
       S0 => NET766,
       S1 => NET782,
       P0 => NET798
  );

NET807 <= NET766 xor NET758;

NET815 <= NET782 xor NET774;

NET823 <= NET798 xor NET790;


end sum_TestBensh;
