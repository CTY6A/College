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
         KEY1_in : IN  std_logic;
         KEY2_in : IN  std_logic;
         KEY3_in : IN  std_logic;
         KEY4_in : IN  std_logic;
         LED_out : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal KEY1_in : std_logic := '0';
   signal KEY2_in : std_logic := '0';
   signal KEY3_in : std_logic := '0';
   signal KEY4_in : std_logic := '0';

 	--Outputs
   signal LED_out : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Button_Led PORT MAP (
          KEY1_in => KEY1_in,
          KEY2_in => KEY2_in,
          KEY3_in => KEY3_in,
          KEY4_in => KEY4_in,
          LED_out => LED_out
        );

   stim_proc: process
   begin		
		wait for 25 ns;
		KEY1_in <= not KEY1_in;
   end process;
	
	stim_proc1: process
   begin		
		wait for 50 ns;
		KEY2_in <= not KEY2_in;
   end process;
	
	stim_proc2: process
   begin		
		wait for 75 ns;
		KEY3_in <= not KEY3_in;
   end process;
	
	stim_proc3: process
   begin		
		wait for 100 ns;
		KEY4_in <= not KEY4_in;
   end process;

END;
