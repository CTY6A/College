----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    14:39:05 11/04/2020 
-- Design Name: 
-- Module Name:    Lab2_sum - Behavioral 
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

entity Lab2_sum is
    Port ( P0 : in  STD_LOGIC;
           A : in  STD_LOGIC;
           B : in  STD_LOGIC;
			  A1 : in  STD_LOGIC;
           B1 : in  STD_LOGIC;
           S : out  STD_LOGIC;
			  S1 : out  STD_LOGIC;
           P1 : out  STD_LOGIC);
end Lab2_sum;

architecture Behavioral of Lab2_sum is
	signal P01: STD_LOGIC;
begin
	P01 <= (P0 and A) or (A and B) or (B and P0);
	S <= (P0 or A) or (A or B);
	S1 <= (P01 or A1) or (A1 or B1);
	P1 <= (P01 and A1) or (A1 and B1) or (B1 and P01);
end Behavioral;

