import i18n from 'i18next'
import { initReactI18next } from 'react-i18next'
import LanguageDetector from 'i18next-browser-languagedetector'

i18n
    // detect user language
    // learn more: https://github.com/i18next/i18next-browser-languageDetector
    .use(LanguageDetector)
    // pass the i18n instance to react-i18next.
    .use(initReactI18next)
    // init i18next
    // for all options read: https://www.i18next.com/overview/configuration-options
    .init({
        debug: true,
        interpolation: {
            escapeValue: false // not needed for react as it escapes by default
        },
        resources: {
            en: {
                translation: {
                    header: {
                        home: 'Home',
                        contacts: 'Contacts',
                        FAQ: 'FAQ',
                        settings: {
                            light: 'Toggle light mode',
                            dark: 'Toggle dark mode'
                        },
                        profile: {
                            profile: 'Profile',
                            logout: 'Logout'
                        },
                        login: 'Login',
                        register: 'Register',
                        manageConsoles: 'Manage Categories',
                        myConsoles: 'My Consoles',
                        borrowConsoles: 'Borrow Consoles',
                        chats: 'Chats',
                        lendRequests: 'Unborrowed consoles',
                        borrowRequests: 'Borrowings',
                        approveRegistrations: 'Approve borrowers'
                    },
                    contacts: {
                        contactInfo: 'Contact Information',
                        businessInfo: 'Business Information',
                        companyCode: 'Company Code: ',
                        VATCode: 'VAT payer code: ',
                        address: 'Address: ',
                        email: 'Email: ',
                        phone: 'Phone: ',
                        fax: 'Fax: ',
                        businessHours1: 'Business hours',
                        businessHours2: 'Mon-Fri - 8am-5pm',
                        businessHours3: 'Sat-Sun - closed',
                        contactUs: 'Contact Us',
                        information: 'Information: ',
                        collaborationProposals: 'Collaboration proposals: ',
                        customerService: 'Customer service: '
                    },
                    faq: {
                        faq: 'Frequently Asked Questions',
                        q1: 'What is the process of signing a contract when renting/leasing a console?',
                        q2: 'What to do if delivery is late?',
                        q3: 'How long will it take for my registration request to be reviewed?',
                        q4: 'Where else can I turn to if the chat functionality of the page stops working?',
                        q5: 'Can a customer change or cancel an order if the shipment is already prepared or in transit?',
                        q6: 'Is it possible to get a refund?',
                        q7: 'What to do if I want to reclaim my console?',
                        q8: 'My console was returned to me with additional defects. Where can I turn in such a case?',
                        q9: 'What is the minimum rental period for a console?',
                        q10: 'How often will I be paid for my console rental?',
                        q11: 'What happens if either party terminates the rental agreement?',
                        a1: 'After submitting the appropriate request on the website, an administrator will contact you for contract matters.',
                        a2: 'Please contact us using the chat functionality of the site or via email at support@nuoma.lt.',
                        a3: 'Within 3 business days.',
                        a4: 'In this case, you can contact us by email suitable for the situation, indicated on the "Contact" page.',
                        a5: 'It is possible to cancel an order only until the shipment has not been sent.',
                        a6: 'For refund issues, please contact us using the chat functionality of the site or via email at support@nuoma.lt.',
                        a7: 'In this case, you can submit a request to return the console on its page. An administrator will contact you later on this matter.',
                        a8: 'For warranty issues, please contact us using the chat functionality of the website or via email at support@nuoma.lt',
                        a9: 'The minimum rental period is 1 month. To terminate the contract, the contract will remain in effect for the current month.',
                        a10: 'Lenders are paid every month.',
                        a11: 'If the console lender terminates the contract, the console is returned to him at the end of the month. If the borrowing company terminates the contract, the console is returned to the warehouse and the lender is contacted whether to terminate the contract or not.'
                    },
                    profile: {
                        personalInfo: 'Personal information',
                        companyInfo: 'Company information',
                        email: 'Email',
                        security: 'Security',
                        saveChanges: 'Save changes',
                        emailStatus: 'Email status',
                        firstName: 'First name',
                        lastName: 'Last name',
                        companyCode: 'Company code',
                        companyName: 'Company name',
                        dataSuccessMessage: 'Data has been updated successfully',
                        address: 'Address'
                    },
                    emailChangeForm: {
                        currentEmail: 'Current email',
                        unconfirmedEmails: 'Unconfirmed emails',
                        newEmail: 'New email address',
                        enterNewEmail: 'Enter new email address',
                        saveChanges: 'Save changes',
                        emailErrorRequired: 'New email adress is required'
                    },
                    passwordChangeForm: {
                        currentPassword: 'Current password',
                        enterCurrentPassword: 'Enter current password',
                        currentPasswordError: 'Current password is required',
                        newPassword: 'New password',
                        enterNewPassword: 'Enter new password',
                        newPasswordError: 'New password is required',
                        shortError: 'Entered password is too short',
                        noDigitError: 'At least 1 digit is required',
                        noLowerError: 'At least 1 lowercase letter is required',
                        noUpperError: 'At least 1 uppercase letter is required',
                        noNonAlphaError: 'At least 1 non-alphanumeric character is required',
                        repeatNewPassword: 'Repeat new password',
                        enterRepeatNewPassword: 'Repeat new password',
                        repeatNewPasswordError: 'Repeating new password is required',
                        passwordMatchError: 'Passwords do not match',
                        saveChanges: 'Save changes',
                        passwordSuccessMessage: 'Password has been changed successfully',
                        passwordFailureMessage: 'Current password is incorrect'
                    },
                    addressForm: {
                        country: 'Country',
                        enterCountry: 'Enter country',
                        countryError: 'Country is required',
                        county: 'County',
                        enterCounty: 'Enter county',
                        countyError: 'County is required',
                        city: 'City',
                        enterCity: 'Enter city name',
                        cityError: 'City is required',
                        streetAddress: 'Street address',
                        enterStreetAddress: 'Enter street address',
                        streetAddressError: 'Street address is required',
                        postalCode: 'Postal code',
                        enterPostalCode: 'Enter postal code',
                        postalCodeError: 'Postal code is required',
                        saveChanges: 'Save changes',
                        addressSuccessMessage: 'Address has been updated successfully'
                    },
                    home: {
                        title1: 'Console Rental',
                        title2: 'We act as intermediaries between console renters',
                        selectBorrowText: 'Is your business in need of console rentals?',
                        selectBorrowButton: 'For Borrowers',
                        selectLendText:
                            'Do you have an unused console and want to earn some money?',
                        selectLendButton: 'For Lenders',
                        borrowers1: 'For Borrowers',
                        borrowers2:
                            'Our business offers you a great opportunity to get a new source of consoles for rent. We are a specialized company that provides console rental services for a fixed daily rate. This offer will allow your customers to enjoy the latest game consoles without significant investments on your part.',

                        borrowers4:
                            'At our place, you will find a wide selection of console accessories to meet all customer needs.',
                        borrowers5:
                            'Our daily rate system is transparent and simple. You just need to choose the desired console model, rental duration, and the accessories that interest you. We ensure that you will receive a quality console with all the necessary accessories.',
                        borrowers6:
                            'Our team is ready to provide you with all the necessary assistance and advice regarding console and accessory selection and ordering.',
                        borrowers3: 'Submit registration request',
                        lenders1: 'For Lenders',
                        lenders5:
                            'If you have a rarely used console, you can offer it on our rental platform. We will give you the opportunity to rent out your console for a fixed daily rate, which will depend on the console model and condition. This will allow you to recoup the money you invested in the console while enabling another player to enjoy the world of gaming.',
                        lenders2:
                            "Do you have rarely used consoles? Maybe you bought a console for a specific game and don't use it anymore? Or do you just want to get your money back for it? In that case, this platform is for you.",
                        lenders3:
                            'Our platform also ensures a secure and reliable rental process. We take care to protect your console from possible damage or loss and ensure that its condition is checked before and after the rental. This helps to ensure that both the renter and the tenant have a positive experience using our platform.',
                        lenders4: 'You can register now'
                    },
                    login: {
                        login: 'Login',
                        username: 'Email',
                        password: 'Password',
                        rememberMe: 'Remember Me',
                        forgotPassword: 'Forgot Password?'
                    },
                    register: {
                        title: 'Registration',
                        titleBorrowers: 'Borrower registration',
                        asCompany: 'Register as company?',
                        asBorrower: 'Do you wish borrow consoles?',
                        companyCode: 'Company Code',
                        companyCodeError: 'Company Code is required',
                        companyName: 'Company Name',
                        companyNameError: 'Company Name is required',
                        firstName: 'First Name',
                        firstNameError: 'First Name is required',
                        lastName: 'Last Name',
                        lastNameError: 'Last Name is required',
                        email: 'Email Address',
                        emailError: 'Email Address is required',
                        password: 'Password',
                        passwordError: 'Password is required',
                        passwordMismatchError: 'Passwords do not match',
                        repeatPassword: 'Repeat Password',
                        registerButton: 'Register',
                        requestButton: 'Submit',
                        checkEmail: 'Please check your email for confirmation link',
                        requestSuccess:
                            'Registration request has been submitted successfully. Please wait for confirmation via email',
                        noRequests: 'Currently there are no registration requests',
                        approve: 'Confirm',
                        reject: 'Reject'
                    },
                    footer: {
                        contacts: 'Contacts',
                        faq: 'Frequently asked questions',
                        register: 'Lender registration page',
                        registerRequest: 'Submit borrower registration request',
                        languages: 'Available languages: '
                    },
                    consoleManagementForm: {
                        console: 'Console Information',
                        name: 'Name',
                        nameError: 'Name is required',
                        description: 'Description',
                        descriptionError: 'Description is required',
                        dailyPrice: 'Daily Price',
                        dailyPriceError: 'Daily Price is required',
                        invalidPriceError: 'Daily Price is invalid',
                        images: 'Images',
                        imagesError: 'At least 2 images are required',
                        update: 'Update',
                        create: 'Create',
                        newConsole: 'New Console',
                        new: 'New'
                    },
                    userConsoleManagementForm: {
                        title: 'Console Information',
                        amount: 'Console amount',
                        amountError: 'Amount is required',
                        invalidAmountError: 'Amount is invalid',
                        accessories: 'Accessories',
                        accessoriesError: 'Accessories is required',
                        accessoriesTooltip: 'E.g.: 3 controllers',
                        consoleCategory: 'Console Category',
                        consoleCategoryError: 'Console Category is required',
                        images: 'Images',
                        imagesError: 'At least 2 images are required',
                        invalidFileError: 'At least 1 uploaded file is not an image',
                        update: 'Update',
                        create: 'Create',
                        newConsole: 'New Console',
                        new: 'New',
                        selectImages: 'Select images',
                        imagesL: 'images',
                        noImage: 'No images selected'
                    },
                    userConsolePage: {
                        consoleTitle: 'About console',
                        consoleName: 'Name',
                        consoleDescription: 'Description',
                        consoleIncome: 'Average monthly income (1 unit)',
                        lendTitle: 'Product details',
                        lendAmount: 'Amount (units)',
                        lendAccessories: 'Total accessories',
                        lendStatus: 'Current status',
                        userTitle: 'About lender',
                        userFname: 'First name',
                        userLname: 'Last name',
                        userEmail: 'Email',
                        userAddress: 'Address',
                        userPostalCode: 'Postal Code:',
                        contact: 'To console conversation',
                        borrowerTitle: 'Borrower company',
                        borrowerCompanyName: 'Company Name',
                        borrowerCompanyCode: 'Company Code',
                        borrowerEmail: 'Email',
                        initiateTermination: 'Initiate console return',
                        statusUnconfirmed: 'No contract',
                        statusAtPlatform: 'Awaiting to be lended',
                        statusReserved: 'Reserved',
                        statusAtLender: 'Rented',
                        statusTerminating: 'Awaiting contract termination',
                        statusTerminatingLender: 'Awaiting to get back',
                        statusTerminatingBorrower: 'Awaiting to return',
                        changeStatus: 'Awaiting for return',
                        dailyPrice: 'Daily price',
                        dailyPriceLender: '30 days income',
                        delete: 'Delete'
                    },
                    borrowerConsolePage: {
                        selectConsole: 'Select',
                        noConsoles: 'There are currently no consoles available'
                    },
                    emailConfirmation: {
                        success: 'The email has been confirmed. You can now ',
                        login: 'login',
                        failure: 'The email confirmation code is invalid or expired.'
                    },
                    passwordReset: {
                        passwordReset: 'Reset password',
                        resetPassword: 'Reset password',
                        sendEmail: 'Send email',
                        success: 'The password has been successfully updated',
                        failure: 'The password reset link is invalid or expired',
                        email: 'Email address',
                        emailError: 'Email Address is required'
                    },
                    emailChange: {
                        success: 'The email has been changed. You can now ',
                        login: 'login',
                        failure: 'The email change code is invalid or expired.'
                    },
                    button: {
                        dialogTitle1: 'Confirmation message',
                        dialogBody1: 'Do you really wish to terminate the contract?',
                        dialogBody2: 'Do you really wish to change the console state?',
                        dialogBody3: 'Do you really wish to select this console for borrow?',
                        dialogBody4: 'Do you really wish to delete this console?',
                        dialogBody5: 'Do you really wish to delete this console category?',
                        dialogBody6: 'Do you really wish to delete this borrowing?',
                        dialogBody7:
                            'Do you really wish to initiate the borrowing of these consoles?',
                        dialogBody8: 'Do you really wish to lend this console(-s)?',
                        dialogBody9: 'Do you really wish to create this console category?',
                        dialogBody10: 'Do you really wish to activate this borrowing?',
                        dialogBody11: 'Do you really wish to mark this borrowing as complete?',
                        confirm: 'Yes',
                        deny: 'No',
                        toUserConsole: 'To user console',
                        toBorrowing: 'To borrowing'
                    },
                    borrowing: {
                        statusPending: 'Awaiting contract signing',
                        statusActive: 'Active',
                        statusTerminating: 'Awaiting consoles return',
                        statusTerminated: 'Terminated',
                        contactBorrower: 'To conversation',
                        borrowing: 'Borrowing',
                        getStatusPending: 'Status: pending',
                        setStatusActive: 'Activate borrowing',
                        getStatusActive: 'Status: active',
                        setStatusTerminating: 'Mark borrowing as terminated',
                        getStatusTerminating: 'Status: awaiting termination'
                    },
                    forgotPassword: {
                        checkEmail: 'Please check your email',
                        emailNotExist: 'This email does not have an account'
                    }
                }
            },
            lt: {
                translation: {
                    header: {
                        home: 'Namai',
                        contacts: 'Kontaktai',
                        FAQ: 'DUK',
                        settings: {
                            light: 'Šviesus rėžimas',
                            dark: 'Tamsus rėžimas'
                        },
                        profile: {
                            profile: 'Profilis',
                            logout: 'Atsijungti'
                        },
                        login: 'Prisijungimas',
                        register: 'Registracija',
                        manageConsoles: 'Kategorijų valdymas',
                        myConsoles: 'Mano konsolės',
                        borrowConsoles: 'Išnuomoti konsoles',
                        chats: 'Pokalbiai',
                        lendRequests: 'Neskolinamos konsolės',
                        borrowRequests: 'Skolinimai',
                        approveRegistrations: 'Registracijos užklausos'
                    },
                    contacts: {
                        contactInfo: 'Kontaktinė informacija',
                        businessInfo: 'Rekvizitai',
                        companyCode: 'Įmonės kodas: ',
                        VATCode: 'PVM mokėtojo kodas: ',
                        address: 'Adresas: ',
                        email: 'El paštas: ',
                        phone: 'Telefonas: ',
                        fax: 'Faksas: ',
                        businessHours1: 'Darbo laikas',
                        businessHours2: 'I-V - 8-17h',
                        businessHours3: 'VI-VII - nedirbame',
                        contactUs: 'Galite susisiekti',
                        information: 'Informacija: ',
                        collaborationProposals: 'Bendradarbiavimo pasiūlymai: ',
                        customerService: 'Klientų aptarnavimas: '
                    },
                    faq: {
                        faq: 'Dažnai užduodami klausimai',
                        q1: 'Kokia sutarties pasirašymo eiga išnuomavus/išsinuomavus konsolę(-es)?',
                        q2: 'Ką daryti, jei pristatymas vėluoja?',
                        q3: 'Per kiek laiko bus peržiūrėtas mano registracijos prašymas?',
                        q4: 'Kur dar galima kreiptis jeigu puslapio pokalbių funkcionalumas nustotų veikti?',
                        q5: 'Ar klientas gali pakeisti arba nutraukti užsakymą jei siunta jau paruošta arba keliauja?',
                        q6: 'Ar įmanomas pinigų atgavimas?',
                        q7: 'Kada daryti jeigu noriu atgauti savo konsolę?',
                        q8: 'Mano konsolė man buvo grąžinta su papildomais defektais. Kur galima kreipti tokiu atveju?',
                        q9: 'Kokia yra minimali konsolės nuomos trukmė?',
                        q10: 'Kaip dažnai man bus išmokami pinigai už mano konsolės nuomą?',
                        q11: 'Kas vyksta jei bent viena pusė nutraukia nuomos sutartį?',
                        a1: 'Pateikus atitinkamą prašymą svetainėje, dėl sutarties reikalų su jumis susisieks administratorius.',
                        a2: 'Prašome kreiptis į mus naudojant puslapio pokalbių funkcionalumą arba el. paštu support@nuoma.lt.',
                        a3: 'Per 3 darbo dienas.',
                        a4: 'Tokiu atveju galite kreiptis situacijai tinkamiausiu el. paštu, nurodytu puslapyje "Kontaktai"',
                        a5: 'Užsakymą atšaukti galima tik kol siunta dar nėra išsiųsta.',
                        a6: 'Pinigų atgavimo klausimais prašome kreiptis naudojant puslapio pokalbių funkcionalumą arba el. paštu support@nuoma.lt.',
                        a7: 'Tokioje situacijoje galite pateikti prašymą grąžinti konsolę jos puslapyje. Administratorius vėliau susisieks su jumis dėl šio klausimo.',
                        a8: 'Dėl garantijos klausimų prašome kreiptis į mus naudojant svetainės pokalbių funkcionalumą arba el. paštu support@nuoma.lt',
                        a9: 'Minimalus nuomos laikotarpis yra 1 mėnuo. Norint nutraukti sutartį, sutartis bus galiojanti iki einamojo mėnesio pabaigos.',
                        a10: 'Nuomotojams pinigai yra išmokami kiekvieną mėnesį',
                        a11: 'Jeigu konsolės skolintojas nutraukia sutartį, jam konsolė grąžinama mėnesio pabaigoje. Jeigu besiskolinanti įmonė nutraukia sutartį, konsolė yra grąžinama į sandėlį ir susisiekiama su skolintoju ar nutraukti sutartį, ar ne.'
                    },
                    addressForm: {
                        country: 'Šalis',
                        enterCountry: 'Įveskite šalį',
                        countryError: 'Šalis yra privaloma',
                        county: 'Apskritis',
                        enterCounty: 'Įveskite apskritį',
                        countyError: 'Apskritis yra privaloma',
                        city: 'Miestas',
                        enterCity: 'Įveskite miestą',
                        cityError: 'Miestas yra privalomas',
                        streetAddress: 'Gatvės adresas',
                        enterStreetAddress: 'Įveskite gatvės adresą',
                        streetAddressError: 'Gatvės adresas yra privalomas',
                        postalCode: 'Pašto kodas',
                        enterPostalCode: 'Įveskite pašto kodą',
                        postalCodeError: 'Pašto kodas yra privalomas',
                        saveChanges: 'Išsaugoti pakeitimus',
                        addressSuccessMessage: 'Adresas atnaujintas sėkmingai'
                    },
                    profile: {
                        personalInfo: 'Asmeninė informacija',
                        companyInfo: 'Įmonės informacija',
                        email: 'El. pašto adresas',
                        security: 'Sauga',
                        saveChanges: 'Išsaugoti pakeitimus',
                        emailStatus: 'El pašto būsena',
                        firstName: 'Vardas',
                        lastName: 'Pavardė',
                        companyCode: 'Įmonės kodas',
                        companyName: 'Įmonės pavadinimas',
                        dataSuccessMessage: 'Duomenys atnaujinti sėkmingai',
                        address: 'Adresas'
                    },
                    emailChangeForm: {
                        currentEmail: 'Dabartinis el. pašto adresas',
                        unconfirmedEmails: 'Nepatvirtinti el. pašto adresai',
                        newEmail: 'Naujas el. pašto adresas',
                        enterNewEmail: 'Įveskite naują el. pašto adresą',
                        saveChanges: 'Išsaugoti pakeitimus',
                        emailErrorRequired: 'Naujas el. pašto adresas yra privalomas'
                    },
                    passwordChangeForm: {
                        currentPassword: 'Dabartinis slaptažodis',
                        enterCurrentPassword: 'Įveskite dabartinį slaptažodį',
                        currentPasswordError: 'Dabartinis slaptažodis yra privalimas',
                        newPassword: 'Naujas slaptažodis',
                        enterNewPassword: 'Įveskite naują slaptažodį',
                        newPasswordError: 'Naujas slaptažodis yra privalomas',
                        shortError: 'Įvestas slaptažodis yra per trumpas',
                        noDigitError: 'Reikia įvesti bent 1 skaičių',
                        noLowerError: 'Reikia įvesti bent 1 mažąją raidę',
                        noUpperError: 'Reikia įvesti bent 1 didžiąją raidę',
                        noNonAlphaError: 'Reikia įvesti bent 1 specialųjį simbolį',
                        repeatNewPassword: 'Pakartokite naują slaptažodį',
                        enterRepeatNewPassword: 'Pakartokite naują slaptažodį',
                        repeatNewPasswordError: 'Naujo slaptažodžio pakartojimas yra privalomas',
                        saveChanges: 'Save changes',
                        passwordSuccessMessage: 'Slaptažodis pakeistas sėkmingai',
                        passwordFailureMessage: 'Neteisingas dabartinis slaptažodis',
                        passwordMatchError: 'Pateikti slaptažodžiai nesutampa'
                    },
                    home: {
                        title1: 'Konsolių nuoma',
                        title2: 'Tarpininkaujame tarp konsolių nuomotojų',
                        selectBorrowText: 'Jūsų verslui trūksta konsolių nuomai?',
                        selectBorrowButton: 'Besiskolinantiems',
                        selectLendText: 'Turite nenaudojamą konsolę ir norite uždirbti?',
                        selectLendButton: 'Skolintojams',
                        borrowers1: 'Besiskolinantiems',
                        borrowers2:
                            'Mūsų verslas siūlo jums puikią galimybę gauti naują konsolių šaltinį nuomai. Mes esame specializuota įmonė, kuri teikia konsoles nuomos paslaugas už fiksuotą dieninį tarifą. Šis pasiūlymas leis jūsų klientams mėgautis naujausiomis žaidimų konsolėmis be didelių investicijų jums.',

                        borrowers4:
                            'Pas mus rasite platų konsolei priedų pasirinkimą, kad galėtumėte patenkinti visus klientų poreikius.',
                        borrowers5:
                            'Mūsų dieninio tarifo sistema yra skaidri ir paprasta. Jums tereikia pasirinkti norimą konsoles modelį, nuomos trukmę ir priedus, kurie jus domina. Mes užtikriname, kad gausite kokybišką konsolę su visais reikalingais priedais.',
                        borrowers6:
                            'Mūsų komanda yra pasiruošusi suteikti jums visą reikiamą pagalbą ir patarimus dėl konsolės ir priedų pasirinkimo bei užsakymo.',
                        borrowers3: 'Pateikti registracijos prašymą',
                        lenders1: 'Skolintojams',
                        lenders5:
                            'Mūsų platforma taip pat užtikrina saugią ir patikimą nuomos procesą. Mes rūpinamės, kad jūsų konsolė būtų apdrausta nuo galimų pažeidimų ar netekimo, ir užtikriname, kad jos būklė būtų tikrinama prieš ir po nuomos. Tai padeda užtikrinti, jog tiek nuomotojas, tiek ir nuomininkas turės teigiamą patirtį naudojantis mūsų platforma.',
                        lenders2:
                            'Ar jūs turite retai naudojamų konsolių? Galbūt jums pinigų nusipirkti sekančios kartos konsolei? Ar tiesiog norite atgauti pinigus sumokėtus už ją? Tokiu atveju ši platforma yra skirta jums.',
                        lenders3:
                            'Jei turite retai naudojamą konsolę, galite ją pasiūlyti mūsų nuomos platformoje. Mes suteiksime jums galimybę nuomoti savo konsolę už nustatytą dieninį tarifą, kuris bus priklauso nuo konsolės modelio ir būklės. Tai leis jums atgauti pinigus, kuriuos investavote į konsolę, tuo pačiu padėdamas kitam žaidėjui mėgautis žaidimų pasauliu.',
                        lenders4: 'Galite užsiregistruoti dabar'
                    },
                    login: {
                        login: 'Prisijungimas',
                        username: 'El paštas',
                        password: 'Slaptažodis',
                        rememberMe: 'Prisimink mane',
                        forgotPassword: 'Pamiršai slaptažodį?'
                    },
                    register: {
                        title: 'Registracija',
                        titleBorrowers: 'Registracija besiskolinantiems',
                        asCompany: 'Norite registruotis kaip įmonė?',
                        asBorrower: 'Ieškote konsolių savo verslui?',
                        companyCode: 'Įmonės kodas',
                        companyCodeError: 'Reikia įvesti įmonės kodą',
                        companyName: 'Įmonės pavadinimas',
                        companyNameError: 'Reikia įvesti įmonės pavadinimą',
                        firstName: 'Vardas',
                        firstNameError: 'Reikia įvesti vardą',
                        lastName: 'Pavardė',
                        lastNameError: 'Reikia įvesti pavardę',
                        email: 'El. paštas',
                        emailError: 'Reikia įvesti el. pašto adresą',
                        password: 'Slaptažodis',
                        passwordError: 'Reikia įvesti slaptažodį',
                        passwordMismatchError: 'Slaptažodžiai nesutampa',
                        repeatPassword: 'Pakartoti slaptažodį',
                        registerButton: 'Registruotis',
                        requestButton: 'Pateikti',
                        checkEmail:
                            'Išsiųsta patvirtinimo nuorodą. Prašome patikrinti savo el. paštą',
                        requestSuccess:
                            'Registracijos prašymas pateiktas sėkmingai. Prašome laukti patvirtinimo el. paštu.',
                        noRequests: 'Šiuo metu registracijos užklausų nėra',
                        approve: 'Patvirtinti',
                        reject: 'Atmesti'
                    },
                    footer: {
                        contacts: 'Kontaktai',
                        faq: 'Dažnai užduodami klausimai',
                        register: 'Skolintojų registracijos puslapis',
                        registerRequest: 'Pateikti besiskolinančio registracijos prašymą',
                        languages: 'Galimos kalbos: '
                    },
                    consoleManagementForm: {
                        console: 'Konsolės informacija',
                        name: 'Pavadinimas',
                        nameError: 'Reikia nurodyti pavadinimą',
                        description: 'Aprašymas',
                        descriptionError: 'Reikia nurodyti aprašymą',
                        dailyPrice: 'Dieninis tarifas',
                        dailyPriceError: 'Reikia nurodyti dieninį tarifą',
                        invalidPriceError: 'Netinkamas skaičius',
                        images: 'Nuotraukos',
                        imagesError: 'Reikia įkelti bent 2 nuotraukas',
                        update: 'Atnaujinti',
                        create: 'Sukurti',
                        newConsole: 'Nauja konsolė',
                        new: 'Pridėti naują'
                    },
                    userConsoleManagementForm: {
                        title: 'Konsolės informacija',
                        amount: 'Konsolių kiekis',
                        amountError: 'Reikia nurodyti kiekį',
                        invalidAmountError: 'Neteisingas kiekis',
                        accessories: 'Priedai',
                        accessoriesError: 'Reikia nurodyti konsolės priedus',
                        accessoriesTooltip: 'Pvz.: 3 pulteliai',
                        consoleCategory: 'Konsolės kategorija',
                        consoleCategoryError: 'Reikia nurodyti konsolės kategoriją',
                        images: 'Nuotraukos',
                        imagesError: 'Reikia įkelti bent 2 nuotraukas',
                        invalidFileError: 'Bent 1 įkeltas failas nėra nuotrauka',
                        update: 'Atnaujinti',
                        create: 'Sukurti',
                        newConsole: 'Nauja konsolė',
                        new: 'Pridėti konsolę',
                        selectImages: 'Įkelti nuotraukas',
                        imagesL: 'nuotraukos',
                        noImage: 'Nepasirinkta jokia nuotrauka'
                    },
                    userConsolePage: {
                        consoleTitle: 'Apie konsolę',
                        consoleName: 'Pavadinimas',
                        consoleDescription: 'Aprašymas',
                        consoleIncome: 'Vidutinės mėnesio pajamos (1vnt.)',
                        lendTitle: 'Nuomos detalės',
                        lendAmount: 'Nuomojamas kiekis (vnt.)',
                        lendAccessories: 'Iš viso priedų',
                        lendStatus: 'Dabartinė būsena',
                        userTitle: 'Apie skolintoją',
                        userFname: 'Vardas',
                        userLname: 'Pavardė',
                        userEmail: 'El. paštas',
                        userAddress: 'Adresas',
                        userPostalCode: 'Pašto kodas:',
                        borrowerTitle: 'Pasiskolinusi įmonė',
                        borrowerCompanyName: 'Įmonės pavadinimas',
                        borrowerCompanyCode: 'Įmonės kodas',
                        borrowerEmail: 'El. paštas',
                        initiateTermination: 'Inicijuoti konsolės grąžinimą',
                        contact: 'Į konsolės pokalbį',
                        borrowingContact: 'Į pasiskolinimo pokalbį',
                        statusUnconfirmed: 'Nepasirašyta sutartis',
                        statusAtPlatform: 'Laukiama nuomos',
                        statusReserved: 'Rezervuota',
                        statusAtLender: 'Nuomojama',
                        statusTerminating: 'Laukiama sutarties nutraukimo',
                        statusTerminatingLender: 'Norima atgauti',
                        statusTerminatingBorrower: 'Norima grąžinti',
                        changeStatus: 'Pakeisti būseną',
                        dailyPrice: 'Dienos kaina',
                        dailyPriceLender: '30 dienų pajamos',
                        delete: 'Ištrinti'
                    },
                    borrowerConsolePage: {
                        selectConsole: 'Pasirinkti',
                        noConsoles: 'Šiuo metu nėra konsolių, prieinamų nuomai'
                    },
                    emailConfirmation: {
                        success: 'Elektroninis paštas sėkmingai patvirtintas. Dabar galite ',
                        login: 'prisijungti',
                        failure:
                            'Elektroninio pašto patvirtinimo kodas yra neteisingas arba pasibaigusio galiojimo.'
                    },
                    passwordReset: {
                        passwordReset: 'Slaptažodžio pakeitimas',
                        resetPassword: 'Pakeisti slaptažodį',
                        sendEmail: 'Išsiųsti el. laišką',
                        success: 'Slaptažodis sėkmingai atnaujintas',
                        failure:
                            'Slaptažodžio pakeitimo nuoroda yra neteisinga arba pasibaigusiu galiojimo',
                        email: 'El. pašto adresas',
                        emailError: 'Įveskite el. pašto adresą'
                    },
                    emailChange: {
                        success: 'Elektroninis paštas sėkmingai pakeistas. Dabar galite ',
                        login: 'prisijungti',
                        failure:
                            'Elektroninio pašto pakeitimo kodas yra neteisingas arba pasibaigusio galiojimo.'
                    },
                    button: {
                        dialogTitle1: 'Patvirtinimo žinutė',
                        dialogBody1: 'Ar tikrai norite inicijuoti konsolės grąžinimą?',
                        dialogBody2: 'Ar tikrai norite pakeisti konsolės būseną?',
                        dialogBody3: 'Ar tikrai norite pasirinkti šią konsolę?',
                        dialogBody4: 'Ar tikrai norite ištrinti šią konsolę?',
                        dialogBody5: 'Ar tikrai norite ištrinti šią konsolių kategoriją?',
                        dialogBody6: 'Ar tikrai norite ištrinti šį pasiskolinimą?',
                        dialogBody7: 'Ar tikrai norite inicijuoti šių konsolių pasiskolinimą?',
                        dialogBody8: 'Ar tikrai norite paleisti šią konsolę į nuomą?',
                        dialogBody9: 'Ar tikrai norite sukurti šią konsolių kategoriją?',
                        dialogBody10: 'Ar tikrai norite aktyvuoti šį pasiskolinimą?',
                        dialogBody11: 'Ar tikrai norite pažymėti šį pasiskolinimą pasibaigusiu?',
                        confirm: 'Taip',
                        deny: 'Ne',
                        toUserConsole: 'Į konsolės puslapį',
                        toBorrowing: 'Į pasiskolinimo puslapį'
                    },
                    borrowing: {
                        borrowing: 'Pasiskolinimas',
                        statusPending: 'Laukiama sutarties pasirašymo',
                        statusActive: 'Aktyvu',
                        statusTerminating: 'Laukiama konsolių grąžinimo',
                        statusTerminated: 'Sutartis nutraukta',
                        contactBorrower: 'Į pokalbį',
                        getStatusPending: 'Pasiskolinimas nepatvirtintas',
                        setStatusActive: 'Aktyvuoti pasiskolinimą',
                        getStatusActive: 'Pasiskolinimas aktyvus',
                        setStatusTerminating: 'Pažymėti pasiskolinimą pasibaigusiu',
                        getStatusTerminating: 'Pasiskolinimas pasibaigęs'
                    },
                    forgotPassword: {
                        checkEmail: 'Prašome patikrinti savo el. paštą',
                        emailNotExist: 'Šis elektroninis paštas neturi paskyros'
                    }
                }
            }
        }
    })

export default i18n
