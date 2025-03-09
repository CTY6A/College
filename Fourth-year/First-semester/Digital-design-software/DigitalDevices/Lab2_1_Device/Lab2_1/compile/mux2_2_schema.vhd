-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\mux2_2_schema.vhd
-- Generated   : Fri Oct 16 13:36:12 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\mux2_2_schema.bde
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

entity mux2_2_schema is
  port(
       a : in STD_LOGIC;
       b : in STD_LOGIC;
       s : in STD_LOGIC;
       a1 : in STD_LOGIC;
       b1 : in STD_LOGIC;
       z : out STD_LOGIC;
       z1 : out STD_LOGIC
  );
end mux2_2_schema;

architecture mux2_2_schema of mux2_2_schema is

---- Component declarations -----

component mux2_schema
  port(
       a : in STD_LOGIC;
       b : in STD_LOGIC;
       s : in STD_LOGIC;
       z : out STD_LOGIC
  );
end component;

begin

----  Component instantiations  ----

U1 : mux2_schema
  port map(
       a => a,
       b => b,
       s => s,
       z => z
  );

U2 : mux2_schema
  port map(
       a => a1,
       b => b1,
       s => s,
       z => z1
  );


end mux2_2_schema;
