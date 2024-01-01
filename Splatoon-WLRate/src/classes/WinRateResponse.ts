export default class WinRateResponse {
  constructor(isValid: boolean, winPercentage?: string, wonMatches?: string) {
    this.IsValid = isValid;
    this.WinPercentage = winPercentage ?? '';
    this.WonMatches = wonMatches ?? '';
  }

  // Indicates if data should be be displayed
  public IsValid: boolean;

  // Percentage of won matches in the last 24 hours
  public WinPercentage: string;

  // The number of winning matches in relation to all matches
  public WonMatches: string;
}
