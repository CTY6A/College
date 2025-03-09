----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    13:55:48 11/08/2020 
-- Design Name: 
-- Module Name:    D_Enable_Latch_Struct_with_delay - Behavioral 
-- Project Name: 
-- Target Devices: 
-- Tool versions: 
-- Description: 
--
-- Dependencies: 
--
-- Revision: 
-- Revision 0.01 - File Created
-- Additional Comments: 
--
----------------------------------------------------------------------------------
library IEEE;
use IEEE.STD_LOGIC_1164.ALL;

-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--use IEEE.NUMERIC_STD.ALL;

-- Uncomment the following library declaration if instantiating
-- any Xilinx primitives in this code.
--library UNISIM;
--use UNISIM.VComponents.all;

entity D_Enable_Latch_Struct_with_delay is
    Port ( D : in  STD_LOGIC;
           E : in  STD_LOGIC;
           Q : out  STD_LOGIC;
           nQ : out  STD_LOGIC);
end D_Enable_Latch_Struct_with_delay;

architecture Struct of D_Enable_Latch_Struct_with_delay is 
	component NOR2 
		port (
			A, B: in STD_LOGIC;
			Z : out STD_LOGIC);
	end component;
	component INV
		port (
			A: in STD_LOGIC;
			Z: out STD_LOGIC);
	end component;
	component AND2 
		port (
			A, B: in STD_LOGIC;
			Z : out STD_LOGIC);
	end component;
	signal nD, S, t1, t2, R : STD_LOGIC;
begin
	U1: AND2 port map (A => E, B => D, Z => S);
	U2: INV port map (A => D, Z => nD);
	U3: AND2 port map (A => nD, B => e, Z => R);
	U4: NOR2 port map (A => S, B => t2, Z => t1);
	U5: NOR2 port map (A => R, B => t1, Z => t2);
	Q <= t2 after 50 ns;
	nQ <= t1 after 70 ns;
end Struct;

