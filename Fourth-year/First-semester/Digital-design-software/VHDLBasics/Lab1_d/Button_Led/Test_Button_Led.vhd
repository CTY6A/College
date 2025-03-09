--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   13:02:25 10/04/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/LabsPOCP/Lab1_Button_Led/Button_Led/Test_Button_Led.vhd
-- Project Name:  Button_Led
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Button_Led
-- 
-- Dependencies:
-- 
-- Revision:
-- Revision 0.01 - File Created
-- Additional Comments:
--
-- Notes: 
-- This testbench has been automatically generated using types std_logic and
-- std_logic_vector for the ports of the unit under test.  Xilinx recommends
-- that these types always be used for the top-level I/O of a design in order
-- to guarantee that the testbench will bind correctly to the post-implementation 
-- simulation model.
--------------------------------------------------------------------------------
LIBRARY ieee;
USE ieee.std_logic_1164.ALL;
 
-- Uncomment the following library declaration if using
-- arithmetic functions with Signed or Unsigned values
--USE ieee.numeric_std.ALL;
 
ENTITY Test_Button_Led IS
END Test_Button_Led;
 
ARCHITECTURE behavior OF Test_Button_Led IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Button_Led
    PORT(
         KEY11_in : IN  std_logic;
         KEY12_in : IN  std_logic;
         KEY21_in : IN  std_logic;
         KEY22_in : IN  std_logic;
         SELECT_in : IN  std_logic;
         LED1_out : OUT  std_logic;
         LED2_out : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal KEY11_in : std_logic := '0';
   signal KEY12_in : std_logic := '0';
   signal KEY21_in : std_logic := '1';
   signal KEY22_in : std_logic := '1';
   signal SELECT_in : std_logic := '0';

 	--Outputs
   signal LED1_out : std_logic;
   signal LED2_out : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Button_Led PORT MAP (
          KEY11_in => KEY11_in,
          KEY12_in => KEY12_in,
          KEY21_in => KEY21_in,
          KEY22_in => KEY22_in,
          SELECT_in => SELECT_in,
          LED1_out => LED1_out,
          LED2_out => LED2_out
        );

   stim_proc: process
   begin		
		wait for 25 ns;
		KEY11_in <= not KEY11_in;
		KEY21_in <= not KEY21_in;
   end process;
	
	stim_proc1: process
   begin		
		wait for 50 ns;
		KEY12_in <= not KEY12_in;
		KEY22_in <= not KEY22_in;
   end process;
	
	stim_proc3: process
   begin		
		wait for 100 ns;
		SELECT_in <= not SELECT_in;
   end process;

END;
