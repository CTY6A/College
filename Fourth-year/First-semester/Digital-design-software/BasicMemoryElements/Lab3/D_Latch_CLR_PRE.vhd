----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:45:46 11/13/2020 
-- Design Name: 
-- Module Name:    D_Latch_CLR_PRE - Behavioral 
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

entity D_Latch_CLR_PRE is
    Port ( CLR : in  STD_LOGIC;
           PRE : in  STD_LOGIC;
           D : in  STD_LOGIC;
           G : in  STD_LOGIC;
           Q : out  STD_LOGIC);
end D_Latch_CLR_PRE;

architecture Behavioral of D_Latch_CLR_PRE is
signal q_t: STD_LOGIC;
begin

Main: process (CLR, PRE, D, G)
begin
--условие асинхронный сброс
	if CLR = '1' then
		q_t <= '0';
--асинхронной установки
	elsif PRE = '1' then
		q_t <= '1';
--асинхронного разрешения
	elsif G= '1' then
			q_t <= D;
	end if;
end process;

Q <= q_t;

end Behavioral;

