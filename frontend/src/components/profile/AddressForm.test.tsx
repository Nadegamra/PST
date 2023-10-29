/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen, fireEvent, act } from '@testing-library/react';
import { useAuth } from '../../contexts/AuthContext';
import AddressForm from './AddressForm';
import { useTranslation } from 'react-i18next';
import { updateAddress } from '../../api/UsersApi';

// Mocking translations
jest.mock('react-i18next', () => ({
  useTranslation: jest.fn().mockReturnValue({
    t: (str: string) => str,
    i18n: { changeLanguage: jest.fn() }
  }),
}));

// Mock user context value
jest.mock('../../contexts/AuthContext', () => ({
    useAuth: jest.fn(),
    AuthContext: jest.fn()
}));

// Mock API calls
jest.mock('../../api/UsersApi', () => ({
  updateAddress: jest.fn()
}));

describe('AddressForm', () => {
  beforeEach(() => {
    (useTranslation as jest.Mock).mockClear();
    (updateAddress as jest.Mock).mockClear().mockResolvedValue({});
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

  it('should render and update address', async () => {
    render(<AddressForm />);

    // Checking for current address values rendering
    expect(screen.getByPlaceholderText(/addressForm.enterCity/i)).toBeInTheDocument();

    expect(screen.getByPlaceholderText(/addressForm.enterCounty/i)).toBeInTheDocument();


    // Try to change address fields
    const cityInput = screen.getByPlaceholderText('addressForm.enterCity');
    fireEvent.change(cityInput, { target: { value: 'Kaunas' } });

    await act(async () => {
        fireEvent.submit(document.querySelector('form')!);
    });

    expect(updateAddress).toHaveBeenCalledWith({
      country: 'Lietuva',
      county: 'Vilnius',
      city: 'Kaunas', 
      streetAddress: 'Main St 123',
      postalCode: '01234',
    });
    
  });
});
