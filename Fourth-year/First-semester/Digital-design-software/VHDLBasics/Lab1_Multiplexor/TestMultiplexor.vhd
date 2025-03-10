--------------------------------------------------------------------------------
-- Company: 
-- Engineer:
--
-- Create Date:   14:13:28 09/15/2020
-- Design Name:   
-- Module Name:   C:/Users/User/Desktop/Lab1POCP/Lab1/TestMultiplexor.vhd
-- Project Name:  Lab1
-- Target Device:  
-- Tool versions:  
-- Description:   
-- 
-- VHDL Test Bench Created by ISE for module: Multiplexor
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
 
ENTITY TestMultiplexor IS
END TestMultiplexor;
 
ARCHITECTURE behavior OF TestMultiplexor IS 
 
    -- Component Declaration for the Unit Under Test (UUT)
 
    COMPONENT Multiplexor
    PORT(
         A : IN  std_logic;
         B : IN  std_logic;
         S : IN  std_logic;
         Z : OUT  std_logic
        );
    END COMPONENT;
    

   --Inputs
   signal A : std_logic := '0';
   signal B : std_logic := '0';
   signal S : std_logic := '0';

 	--Outputs
   signal Z : std_logic;
   -- No clocks detected in port list. Replace <clock> below with 
   -- appropriate port name 
 
   --constant <clock>_period : time := 10 ns;
 
BEGIN
 
	-- Instantiate the Unit Under Test (UUT)
   uut: Multiplexor PORT MAP (
          A => A,
          B => B,
          S => S,
          Z => Z
        );

   -- Clock process definitions
   --<clock>_process :process
   --begin
	--	<clock> <= '0';
	--	wait for <clock>_period/2;
	--	<clock> <= '1';
	--	wait for <clock>_period/2;
   --end process;
 

   -- Stimulus process
   stim_proc: process
   begin		
		wait for 50 ns;
		A <= not A;
   end process;
	
	stim_proc1: process
   begin		
		wait for 25 ns;
		B <= not B;
   end process;
	
	stim_proc2: process
   begin		
		wait for 100 ns;
		S <= not S;
   end process;

END;
