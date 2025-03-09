--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:59:46 11/04/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/LabsPOCP/Lab2_sum/Test_all_sum.vhd
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
 
ENTITY Test_all_sum IS
END Test_all_sum;
 
ARCHITECTURE behavior OF Test_all_sum IS 
 
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
	 
    COMPONENT sum2
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
   signal S_s : std_logic;
   signal S1_s : std_logic;
   signal P1_s : std_logic;
	
	
   signal S_b : std_logic;
   signal S1_b : std_logic;
   signal P1_b : std_logic;
	
	signal Misstake : std_logic;
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   	uut1: sum2 PORT MAP (
          P0 => P0,
          A => A,
          B => B,
          A1 => A1,
          B1 => B1,
          S => S_b,
          S1 => S1_b,
          P1 => P1_b
        );
	
	uut0: Lab2_sum PORT MAP (
          P0 => P0,
          A => A,
          B => B,
          A1 => A1,
          B1 => B1,
          S => S_s,
          S1 => S1_s,
          P1 => P1_s
        );
		  


    stim_proc: process
   begin		
		wait for 25 ns;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
		
		wait for 25 ns;
		A <= not A;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
		
		wait for 25 ns;
		B <= not B;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
		
		wait for 25 ns;
		P0 <= not P0;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
		
		wait for 25 ns;
		A1 <= not A1;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
		
		wait for 25 ns;
		B1 <= not B1;
		Misstake <= (S_s xor S_b) or (S1_s xor S1_b) or (P1_s xor P1_b); 
   end process;

END;
