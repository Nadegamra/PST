/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen, fireEvent, act, waitFor } from '@testing-library/react';
import { useAuth } from '../../contexts/AuthContext';
import EmailChangeForm from './EmailChangeForm';
import { useTranslation } from 'react-i18next';
import { getUnconfirmedEmails, sendEmailChangeToken } from '../../api/UsersApi';

// Mocking translations
jest.mock('react-i18next', () => ({
  useTranslation: jest.fn().mockReturnValue({
    t: (str: string) => str,
    i18n: { changeLanguage: jest.fn() }
  }),
}));

// Mock user context value
jest.mock('../../contexts/AuthContext', () => ({
    useAuth: jest.fn(() => ({
      user: {
        email: 'admin@admin.com'
      }
    })),
    AuthContext: jest.fn()
  }));

// Mock API calls
jest.mock('../../api/UsersApi', () => ({
  getUnconfirmedEmails: jest.fn(),
  sendEmailChangeToken: jest.fn()
}));


describe('EmailChangeForm', () => {
  beforeEach(() => {
    (useTranslation as jest.Mock).mockClear();
    (getUnconfirmedEmails as jest.Mock).mockClear().mockResolvedValue({ data: [] });
    (sendEmailChangeToken as jest.Mock).mockClear().mockResolvedValue({});
    (useAuth as jest.Mock).mockReturnValue({
      user: {
        "id": 1,
        "role": "admin",
        "firstName": "Admy",
        "lastName": "Nisterson",
        "companyCode": "",
        "companyName": "",
        "username": "admin@admin.com",
        "email": "admin@admin.com",
        "emailConfirmed": true,
        "country": "",
        "county": "",
        "city": "",
        "streetAddress": "",
        "postalCode": "",
        "isCompany": false
      }
    });
  });

  it('should render and update email', async () => {
    render(<EmailChangeForm />);

    // Checking for current email rendering
    expect(screen.getByText('admin@admin.com')).toBeInTheDocument();

    // Tryto change email
    const emailInput = screen.getByPlaceholderText('emailChangeForm.enterNewEmail');
    fireEvent.change(emailInput, { target: { value: 'newemail@test.com' } });

    await act(async () => {
        fireEvent.submit(document.querySelector('form')!);
    });

    expect(sendEmailChangeToken).toHaveBeenCalledWith({ newEmail: 'newemail@test.com' });
    
  });
});
