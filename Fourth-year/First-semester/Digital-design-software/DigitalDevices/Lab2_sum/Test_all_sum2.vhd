--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:53:38 11/13/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/LabsPOCP/Lab2_sum/Test_all_sum2.vhd
-- Project Name:  Lab2_sum
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Lab2_sum
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
 
ENTITY Test_all_sum2 IS
END Test_all_sum2;
 
ARCHITECTURE behavior OF Test_all_sum2 IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Lab2_sum
    PORT(
         P0 : IN  std_logic;
         A : IN  std_logic;
         B : IN  std_logic;
         A1 : IN  std_logic;
         B1 : IN  std_logic;
         S : OUT  std_logic;
         S1 : OUT  std_logic;
         P1 : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal P0 : std_logic := '0';
   signal A : std_logic := '0';
   signal B : std_logic := '0';
   signal A1 : std_logic := '0';
   signal B1 : std_logic := '0';

 	--Outputs
   signal S : std_logic;
   signal S1 : std_logic;
   signal P1 : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Lab2_sum PORT MAP (
          P0 => P0,
          A => A,
          B => B,
          A1 => A1,
          B1 => B1,
          S => S,
          S1 => S1,
          P1 => P1
        );
   -- Stimulus process
   stim_proc: process
   begin		
      -- hold reset state for 100 ns.
      wait for 100 ns;	
   end process;

END;
