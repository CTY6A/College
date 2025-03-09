----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    12:58:48 10/04/2020 
-- Design Name: 
-- Module Name:    Button_Led - Behavioral 
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

entity Button_Led is
    Port ( KEY11_in : in  STD_LOGIC;
           KEY12_in : in  STD_LOGIC;
           KEY21_in : in  STD_LOGIC;
           KEY22_in : in  STD_LOGIC;
           SELECT_in : in  STD_LOGIC;
           LED1_out : out  STD_LOGIC;
           LED2_out : out  STD_LOGIC);
end Button_Led;

architecture Behavioral of Button_Led is

begin
	process(KEY11_in, KEY12_in, KEY21_in, KEY22_in) is
	begin
		case SELECT_in is
			when '0' =>
			  LED1_out <= KEY11_in;
			  LED2_out <= KEY12_in;	
			when '1' =>
			  LED1_out <= KEY21_in;
			  LED2_out <= KEY22_in;
			when others =>
			  LED1_out <= '0';
			  LED2_out <= '0';
		end case;
	end process;
end Behavioral;

