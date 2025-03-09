--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   15:52:59 11/13/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/LabsPOCP/Lab3/TFF_Test.vhd
-- Project Name:  Lab3
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: TFF
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
 
ENTITY TFF_Test IS
END TFF_Test;
 
ARCHITECTURE behavior OF TFF_Test IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT TFF
    PORT(
         CLR : IN  std_logic;
         C : IN  std_logic;
         T : IN  std_logic;
         Q : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal CLR : std_logic := '0';
   signal C : std_logic := '0';
   signal T : std_logic := '0';

 	--Outputs
   signal Q : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 
 
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: TFF PORT MAP (
          CLR => CLR,
          C => C,
          T => T,
          Q => Q
        );

   -- Stimulus process
   stim_proc: process
   begin		      
      wait for 100 ns;	
	
		t <= not t;
		
		wait for 75 ns;
		
		C <= not C;
		
		wait for 50 ns;
		
		CLR <= not CLR;
		
   end process;

END;
