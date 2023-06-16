using HomeScene;
using UnityEngine;
public class EscapeWindow : BaseWindowControl
{
    public BaseButton btnHome;
    public BaseButton btnInstruction;

    public DialogHomeScene homeDialog;
    public DialogHomeScene instructionDialog;

    [SerializeField] GameObject _homePrefab;
    [SerializeField] GameObject _instructionPrefab;
    [SerializeField] Transform _extension;
    private void OnEnable()
    {
        if (btnHome)
            btnHome.onClick.AddListener(ShowBackHomeDialog);
        if(btnInstruction)
            btnInstruction.onClick.AddListener(ShowInstructionDialog);
    }
    private void OnDisable()
    {
        if (btnHome)
            btnHome.onClick.RemoveListener(ShowBackHomeDialog);
        if (btnInstruction)
            btnInstruction.onClick.RemoveListener(ShowInstructionDialog);
    }
    private void Update()
    {
        if (_uiInput.escape && !isShowingDialog)
        {
            this.ShowDialog(false);
        }
        else if(_uiInput.escape && isShowingDialog)
        {
            this.CloseDialog();
        }
    }
    private void ShowBackHomeDialog()
    {
        if (homeDialog == null)
            InnitBackHomeDialog();

        homeDialog.ShowDialog();
    }
    private void ShowInstructionDialog()
    {
        if(instructionDialog == null) 
            InnitInstructionDialog();
        
        instructionDialog.ShowDialog();
    }
    private void InnitBackHomeDialog()
    {
        GameObject home = Instantiate(_homePrefab, _extension);
        homeDialog = home.GetComponent<DialogHomeScene>();
    }
    private void InnitInstructionDialog()
    {
        GameObject instruction = Instantiate(_instructionPrefab, _extension);
        instructionDialog = instruction.GetComponent<DialogHomeScene>();
    }
}
