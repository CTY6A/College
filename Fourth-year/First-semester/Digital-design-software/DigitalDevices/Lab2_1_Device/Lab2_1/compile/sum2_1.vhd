-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\sum2_1.vhd
-- Generated   : Fri Oct 16 16:22:18 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\sum2_1.bde
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

entity sum2_TestBench is
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
end sum2_TestBench;

architecture sum2_TestBench of sum2_TestBench is

---- Signal declarations used on the diagram ----

signal NET1119 : STD_LOGIC;
signal NET629 : STD_LOGIC;
signal NET633 : STD_LOGIC;
signal NET635 : STD_LOGIC;
signal NET772 : STD_LOGIC;
signal NET776 : STD_LOGIC;
signal NET778 : STD_LOGIC;

begin

----  Component instantiations  ----

NET629 <= A0 and B0;

P0 <= NET778 or NET776 or NET772;

NET633 <= P1 and B0;

NET635 <= A0 and P1;

S0 <= B0 or A0 or P1;

NET1119 <= NET635 or NET633 or NET629;

NET772 <= A1 and B1;

NET776 <= NET1119 and B1;

NET778 <= A1 and NET1119;

S1 <= B1 or A1 or NET1119;


end sum2_TestBench;
