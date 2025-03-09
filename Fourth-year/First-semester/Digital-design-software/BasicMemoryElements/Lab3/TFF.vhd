----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:39:21 11/13/2020 
-- Design Name: 
-- Module Name:    TFF - Behavioral 
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

entity TFF is
    Port ( CLR : in  STD_LOGIC;
           C : in  STD_LOGIC;
           T : in  STD_LOGIC;
           Q : out  STD_LOGIC);
end TFF;

architecture Behavioral of TFF is
signal s: STD_LOGIC := '0';
begin

Main: process (CLR, T, C, s)
begin
--условие асинхронный сброс
	if CLR = '1' then
		s <= '0';
--услоие управл€ющего сигнала синхронизации
	elsif T = '1' then
--условие наступлени€ переднего фронта синхросигнала
		if rising_edge( C ) then
			s <=  not s;
		end if;
	end if;
end process;

Q <= s;

end Behavioral;
