----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    13:41:25 11/08/2020 
-- Design Name: 
-- Module Name:    RS_Latch_with_delay - Behavioral 
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

entity RS_Latch_with_delay is
    Port ( R : in  STD_LOGIC;
           S : in  STD_LOGIC;
           nQ : out  STD_LOGIC;
           Q : out  STD_LOGIC);
end RS_Latch_with_delay;

architecture Struct of RS_Latch_with_delay is 
	component NOR2 
		port (
			A, B: in STD_LOGIC;
			Z : out STD_LOGIC);
	end component;
	signal t1, t2 : STD_LOGIC;
begin
	U1: NOR2 port map (A => S, B => t2, Z => t1);
	U2: NOR2 port map (A => R, B => t1, Z => t2);
	nQ <= transport t1 after 50 ns;
	Q <= transport t2 after 100 ns;
end Struct;

