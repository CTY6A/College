----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    11:54:17 11/26/2020 
-- Design Name: 
-- Module Name:    Signature - Behavioral 
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

entity Signature is
	generic (i:integer := 2);
	port(
		CLK: in std_logic;
		RST: in std_logic;
		Pin: in std_logic;
		Pout: out std_logic_vector(0 to 2**i-1)
		);
end Signature;

architecture Beh of Signature is
	signal sreg: std_logic_vector(0 to 2**i-1);
	signal sdat: std_logic_vector(0 to 2**i-1);
	signal buf: std_logic;
Begin
	Main: process (CLK, RST, sdat)
	begin
		if RST = '1' then
			sreg <= (others => '0');
		elsif rising_edge(CLK) then
			sreg <= sdat;
		end if;
	end process;
	
	Data: process (Pin, sreg)
	begin
		buf <= sreg(0) xor sreg(2**i-1);
		sdat <= (sreg(2**i-1) xor PIn) & sreg(0 to 2**i-2);
		sdat(2) <= buf;
	end process;
	
	Pout <= sreg;
End Beh;