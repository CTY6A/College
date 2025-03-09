----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    11:20:20 11/26/2020 
-- Design Name: 
-- Module Name:    a - Behavioral 
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

entity DFF_Enable_Async is port (
		Clock: in std_logic;
		Enable: in std_logic;
		Clear: in std_logic;
		D: in std_logic;
		Q: out std_logic);
end DFF_Enable_Async;

architecture Behavioral of DFF_Enable_Async is
	signal t_q: std_logic;
begin
	process (Clock, Clear)
	begin
		if (Clear = '1') then
			t_q <= '0';
		elsif (Clock'event and Clock = '1') then
			if Enable = '1' then
				t_q <= D;
			end if;
		end if;
	end process;
	
	Q <= t_q;
	
end Behavioral;