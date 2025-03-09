----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    11:04:34 11/26/2020 
-- Design Name: 
-- Module Name:    AsyncRegister - Behavioral 
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

entity AsyncRegister is
	 generic (n: integer := 4);
    Port ( Din : STD_LOGIC_VECTOR(n-1 downto 0);
		     EN : in  STD_LOGIC;
           Dout : out std_logic_vector(n-1 downto 0));
end AsyncRegister;

architecture Struct of AsyncRegister is
	component D_Enable_Latch
		port (
			D, E : in std_logic;
			Q : out std_logic
			);
	end component;
	signal buf: std_logic_vector(n-1 downto 0);
Begin
	sch: for i in n-1 downto 0 generate
		U_J: D_Enable_Latch port map(DIn(i), en, buf(i));
	end generate;
	Dout <= buf;
End Struct;

architecture Beh of AsyncRegister is
	signal buf: std_logic_vector(n-1 downto 0);
Begin
	Main: process (Din, EN)
	begin
		if (EN = '1')  then
			buf <= Din;
		end if;
	end process;
	Dout <= buf;
End	Beh;

