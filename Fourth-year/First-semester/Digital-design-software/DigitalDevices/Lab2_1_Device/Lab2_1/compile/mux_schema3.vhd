-------------------------------------------------------------------------------
--
-- Title       : 
-- Design      : Lab2_1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : C:\Users\User\Desktop\Lab2\Lab2_1\compile\mux_schema3.vhd
-- Generated   : Fri Oct 16 13:36:12 2020
-- From        : C:\Users\User\Desktop\Lab2\Lab2_1\src\mux_schema3.bde
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

entity mux_schema3 is
  port(
       W : in STD_LOGIC;
       X : in STD_LOGIC;
       Y : in STD_LOGIC;
       Z : in STD_LOGIC;
       G : out STD_LOGIC
  );
end mux_schema3;

architecture mux_schema3 of mux_schema3 is

---- Signal declarations used on the diagram ----

signal NET656 : STD_LOGIC;
signal NET664 : STD_LOGIC;

begin

----  Component instantiations  ----

NET664 <= not(Y and X and W);

NET656 <= not(Z and Y);

G <= not(NET656) or not(NET664);


end mux_schema3;
