----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    11:30:14 11/26/2020 
-- Design Name: 
-- Module Name:    ShiftReg - Behavioral 
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

entity ShiftReg is
	generic (N: integer:= 3);
	port(
		Din, 
		SE, 
		CLK, 
		RST: in std_logic;
		Dout: out std_logic_vector(N-1 downto 0)
		);
end ShiftReg;				  

architecture Beh of ShiftReg is	 
	signal sdat: std_logic_vector(N-1 downto 0);
	signal sreg: std_logic_vector(n-1 downto 0);
begin
	Main: process (CLK, RST, sdat)
	begin
		if RST = '1' then
			sreg <= (others => '0');
		elsif rising_edge(CLK) then
			sreg <= sdat;
		end if;
	end process;
	
	Data: process (sreg, SE)
	begin
		if (SE = '1') then
			sdat <= Din & sreg(n-1 downto 1);
		end if;
		
	end process;
	
	Dout <= sreg; 
end Beh;

architecture Struct of ShiftReg is
	component DFF_Enable_Async is
		port (
			Clock: in std_logic;
			Enable: in std_logic;
			Clear: in std_logic;
			D: in std_logic;
			Q: out std_logic);
	end component;	  					   
	signal outS: std_logic_vector(N-1 downto 0);
begin			   
	U_0: entity work.DFF_Enable_Async 
	port map(CLK, SE, RST, outS(0));
	SCH: for J in 1 to N-1 generate			
		U_J: entity work.DFF_Enable_Async 
		port map (CLK,SE,RST,outS(J-1),outS(J));
	end generate;				 
	Dout <= outS;
end Struct;
