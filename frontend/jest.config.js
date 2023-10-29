/** @type {import('ts-jest').JestConfigWithTsJest} */
module.exports = {
  preset: 'ts-jest',
  setupFilesAfterEnv: ["@testing-library/jest-dom/extend-expect"],
  moduleNameMapper: {
    "\\.(css|less|scss|sass)$": "C:/Users/Emilis/Desktop/PST-master/frontend/src/__mocks__/fileMock.js"
  }
  
  
};