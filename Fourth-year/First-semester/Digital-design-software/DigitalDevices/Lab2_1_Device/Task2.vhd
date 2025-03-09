----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:11:14 11/03/2020 
-- Design Name: 
-- Module Name:    Task2 - Behavioral 
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

entity Task2 is
    Port ( a : in  STD_LOGIC;
           b : in  STD_LOGIC;
           s : in  STD_LOGIC;
           a1 : in  STD_LOGIC;
           b1 : in  STD_LOGIC;
           z : out  STD_LOGIC;
           z1 : out  STD_LOGIC);
end Task2;

architecture Behavioral of Task2 is

begin
process Mux (a, b, s) is
begin
	Output <= (a and not b) or (b and s);
end process;
	
	
	
end Behavioral;

