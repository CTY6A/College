----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    13:30:32 11/08/2020 
-- Design Name: 
-- Module Name:    BistableElement - Behavioral 
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

entity BistableElement is
    Port ( Q : out  STD_LOGIC;
           nQ : out  STD_LOGIC);
end BistableElement;

architecture Behavioral of BistableElement is
	component Inv
		port (
			a: in STD_LOGIC;
			z: out STD_LOGIC);
	end component;
	signal t1, t2: STD_LOGIC;
begin
	U1: inv port map (a => t2, z => t1);
	U2: inv port map (a => t1, z => t2);
	nQ <= transport t1 after 50 ns;
	Q <= transport t2 after 70 ns;
end Behavioral;

