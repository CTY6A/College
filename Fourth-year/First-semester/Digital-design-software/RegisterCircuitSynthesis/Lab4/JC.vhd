----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    11:51:11 11/26/2020 
-- Design Name: 
-- Module Name:    JC - Behavioral 
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

entity JC is
	generic (i:integer := 2);
	port(
		CLK: in std_logic;
		RST: in std_logic;
		LS: in std_logic;
		Pin: in std_logic_vector(0 to 2**i-1);
		Pout: out std_logic_vector(0 to 2**i-1)
		);
end JC;

architecture Beh of JC is
	signal sreg: std_logic_vector(0 to 2**i-1);
	signal sdat: std_logic_vector(0 to 2**i-1);
Begin
	Main: process (CLK, RST, sdat)
	begin
		if RST = '1' then
			sreg <= (others => '0');
		elsif rising_edge(CLK) then
			sreg <= sdat;
		end if;
	end process;
	
	Data: process (LS, Pin, sreg)
	begin
		if LS = '0' then
			sdat <= Pin;
		else
			sdat <= not(sreg(2**i-1)) & sreg(0 to 2**i-2);
		end if;
	end process;
	
	Pout <= sreg;
End Beh;