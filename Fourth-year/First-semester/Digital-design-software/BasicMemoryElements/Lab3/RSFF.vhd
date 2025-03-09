----------------------------------------------------------------------------------
-- Company: 
-- Engineer: 
-- 
-- Create Date:    15:37:23 11/13/2020 
-- Design Name: 
-- Module Name:    RSFF - Behavioral 
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

entity RSFF is
    Port ( S : in  STD_LOGIC;
           R : in  STD_LOGIC;
           CLK : in  STD_LOGIC;
           Q : out  STD_LOGIC;
           nQ : out  STD_LOGIC);
end RSFF;

architecture Behavioral of RSFF is
signal s: STD_LOGIC;
begin

Main: process (CLR, PRE, CE, C, D)
begin
--������� ����������� �����
	if CLR = '1' then
		s <= '0';
--����������� ���������
	elsif PRE = '1' then
		s <= '1';
--������������ ����������
	elsif CE = '1' then
--������� ����������� ��������� ������ �������������
		if rising_edge( C ) then
			s <= D;
		end if;
	end if;
end process;

Q <= s;

end Behavioral;

