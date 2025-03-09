-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : c:\Users\User\Desktop\Lab2\Lab2_1\compile\mux2_schema.vhd
-- Generated   : Thu Oct 15 21:47:24 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\mux2_schema.bde
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

entity mux2_schema is
  port(
       a : in STD_LOGIC;
       b : in STD_LOGIC;
       s : in STD_LOGIC;
       z : out STD_LOGIC
  );
end mux2_schema;

architecture mux2_schema of mux2_schema is

---- Signal declarations used on the diagram ----

signal NET649 : STD_LOGIC;
signal NET665 : STD_LOGIC;
signal NET671 : STD_LOGIC;

begin

----  Component instantiations  ----

NET665 <= NET649 and a;

NET671 <= b and s;

NET649 <= not(s);

z <= NET671 or NET665;


end mux2_schema;
