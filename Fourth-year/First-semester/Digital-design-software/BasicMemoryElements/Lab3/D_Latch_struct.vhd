----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    13:44:56 11/08/2020 
-- Design Name: 
-- Module Name:    D_Latch_struct - Behavioral 
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

entity D_Latch_struct is
    Port ( D : in  STD_LOGIC;
           Q : out  STD_LOGIC;
           nQ : out  STD_LOGIC);
end D_Latch_struct;

architecture Struct of D_Latch_struct is 
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
	signal t1, t2, R : STD_LOGIC;
begin
	U1: INV port map (A => D, Z => R);
	U2: NOR2 port map (A => D, B => t2, Z => t1);
	U3: NOR2 port map (A => R, B => t1, Z => t2);
	Q <= t2;
	nQ <= t1;
end Struct;

