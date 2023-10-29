/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Header from './Header'; 
import { useAuth } from '../../contexts/AuthContext';
import { BrowserRouter as Router } from 'react-router-dom';



// Mock user context value
jest.mock('../../contexts/AuthContext', () => ({
    useAuth: jest.fn(),
}));

jest.mock('react-i18next', () => ({
    useTranslation: () => {
      return {
        t: (str: string) => str, 
        i18n: {
          changeLanguage: () => new Promise(() => { }),
        },
      };
    },
  }));
  

describe('Header Theme Toggle', () => {
    beforeEach(() => {
        (useAuth as jest.Mock).mockReturnValue({
          user: {
            "id": 1,
            "role": "admin",
            "firstName": "John",
            "lastName": "Doe",
            "companyCode": "",
            "companyName": "",
            "username": "john.doe@example.com",
            "email": "john.doe@example.com",
            "emailConfirmed": true,
            "country": "Lietuva",
            "county": "Vilnius",
            "city": "Vilnius",
            "streetAddress": "Main St 123",
            "postalCode": "01234",
            "isCompany": false
          }
        });
      });
    it('starts with light mode by default', () => {
      render(<Router><Header /></Router>);
      
      // Check for light mode icon, for example:
      expect(screen.getByText('light_mode')).toBeInTheDocument(); 
    });
  
    it('toggles to dark mode when the theme button is clicked', () => {
      render(<Router><Header /></Router>);
      
      // Simulate a click on the theme toggle button
      fireEvent.click(screen.getByText('light_mode')); 
      
      // Check for dark mode
      expect(screen.getByText('dark_mode')).toBeInTheDocument();
    });
  
  });

describe('Header Navigation Links', () => {
    beforeEach(() => {
        (useAuth as jest.Mock).mockReturnValue({
          user: {
            "id": 1,
            "role": "admin",
            "firstName": "John",
            "lastName": "Doe",
            "companyCode": "",
            "companyName": "",
            "username": "john.doe@example.com",
            "email": "john.doe@example.com",
            "emailConfirmed": true,
            "country": "Lietuva",
            "county": "Vilnius",
            "city": "Vilnius",
            "streetAddress": "Main St 123",
            "postalCode": "01234",
            "isCompany": false
          }
        });
      });

    
    afterEach(() => {
        jest.clearAllMocks();
    });


    
    describe('Header Navigation Links', () => {
      beforeEach(() => {
        render(
            <Router>
              <Header />
            </Router>
          );
          
      });
    
      it('displays the Contacts link', () => {
        expect(screen.getByText('header.contacts')).toBeInTheDocument();
      });
    
      it('displays the FAQ link', () => {
        expect(screen.getByText('header.FAQ')).toBeInTheDocument();
      });

      it('displays the Manage Consoles link', () => {
        expect(screen.getByText('header.manageConsoles')).toBeInTheDocument();
      });

      it('displays the Chats link', () => {
        expect(screen.getByText('chat')).toBeInTheDocument();
      });

      it('displays the Approve registrations link', () => {
        expect(screen.getByText('header.approveRegistrations')).toBeInTheDocument();
      });
    });
    
});