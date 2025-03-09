----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:17:26 11/13/2020 
-- Design Name: 
-- Module Name:    DFF_Enable - Behavioral 
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

entity DFF_Enable is
    Port ( D : in  STD_LOGIC;
           C : in  STD_LOGIC;
           E : in  STD_LOGIC;
           Q : out  STD_LOGIC);
end DFF_Enable;

architecture Behavioral of DFF_Enable is
signal s: STD_LOGIC;
begin

Main: process (C, E, D)
begin
	if E = '1' then
		if (C'event and C = '1' ) then
			s <= D;
		end if;
	end if;
end process;

Q <= s;

end Behavioral;

