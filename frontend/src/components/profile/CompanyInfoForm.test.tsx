/**
 * @jest-environment jsdom
 */
import { render, screen, fireEvent } from '@testing-library/react';
import CompanyInfoForm from './CompanyInfoForm';
import { useAuth } from '../../contexts/AuthContext';

jest.mock('react-i18next', () => ({
  useTranslation: jest.fn().mockReturnValue({
    t: (key: string) => key,
  }),
}));

jest.mock('../../contexts/AuthContext', () => ({
  useAuth: jest.fn(),
}));

jest.mock('../../api/UsersApi', () => ({
  updateLegal: jest.fn(),
}));

const mockUser = {
  companyCode: '1234',
  companyName: 'Mocked Company',
};

describe('CompanyInfoForm', () => {
  beforeEach(() => {
    (useAuth as jest.MockedFunction<any>).mockReturnValue({ user: mockUser });
  });

  it('renders correctly with initial values', () => {
    render(<CompanyInfoForm />);
    const companyCodeInput = screen.getByDisplayValue(mockUser.companyCode);
    const companyNameInput = screen.getByDisplayValue(mockUser.companyName);
    expect(companyCodeInput).toBeInTheDocument();
    expect(companyNameInput).toBeInTheDocument();
  });

  it('updates the input values on change', () => {
    render(<CompanyInfoForm />);
    const companyCodeInput = screen.getByDisplayValue(mockUser.companyCode);
    fireEvent.change(companyCodeInput, { target: { value: '5678' } });
    expect(companyCodeInput).toHaveValue('5678');
  });

  it('triggers API call and reflects success message on save', async () => {
    const mockUpdateLegal = require('../../api/UsersApi').updateLegal;
    mockUpdateLegal.mockResolvedValueOnce(true);

    render(<CompanyInfoForm />);
    const saveButton = screen.getByText('profile.saveChanges');
    fireEvent.click(saveButton);

    const successMessage = await screen.findByText('profile.dataSuccessMessage');
    expect(successMessage).toBeInTheDocument();
  });

});