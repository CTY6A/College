----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:29:28 11/13/2020 
-- Design Name: 
-- Module Name:    DFF2 - Behavioral 
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

entity DFF2 is
    Port ( CLR : in  STD_LOGIC;
           PRE : in  STD_LOGIC;
           CE : in  STD_LOGIC;
           C : in  STD_LOGIC;
           D : in  STD_LOGIC;
           Q : out  STD_LOGIC);
end DFF2;

architecture Behavioral of DFF2 is
signal s: STD_LOGIC;
begin

Main: process (CLR, PRE, CE, C, D)
begin
--условие асинхронный сброс
	if CLR = '1' then
		s <= '0';
--асинхронной установки
	elsif PRE = '1' then
		s <= '1';
--асинхронного разрешения
	elsif CE = '1' then
--условие наступления переднего фронта синхросигнала
		if rising_edge( C ) then
			s <= D;
		end if;
	end if;
end process;

Q <= s;

end Behavioral;

