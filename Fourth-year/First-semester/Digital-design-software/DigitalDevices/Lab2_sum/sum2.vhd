----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    14:47:34 11/04/2020 
-- Design Name: 
-- Module Name:    sum2 - Behavioral 
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

entity sum2 is
    Port ( p0 : in  STD_LOGIC;
           a : in  STD_LOGIC;
           b : in  STD_LOGIC;
           a1 : in  STD_LOGIC;
           b1 : in  STD_LOGIC;
           s : out  STD_LOGIC;
           s1 : out  STD_LOGIC;
           p1 : out  STD_LOGIC);
end sum2;

architecture Behavioral of sum2 is
	component sum1 is
	Port ( a : in  STD_LOGIC;
          b : in  STD_LOGIC;
          p0 : in  STD_LOGIC;
          s : out  STD_LOGIC;
          p1 : out  STD_LOGIC);
   end component;
	signal p01: STD_LOGIC;
begin
	
	sum20 : sum1 port map (a => a, b => b, p0 => p0, s => s, p1 => p01);
	sum21 : sum1 port map (a => a1, b => b1, p0 => p01, s => s1, p1 => p1);	

end Behavioral;

