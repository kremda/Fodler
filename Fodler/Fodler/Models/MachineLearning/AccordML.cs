using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
using Accord.MachineLearning.Bayes;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Filters;
using Accord.Statistics.Kernels;
using Fodler.Helpers;

namespace Fodler.Models.MachineLearning
{
    public class AccordML
    {
        private BagOfWords _inputBagOfWords;
        private BagOfWords _subjectBagOfWords;
        private BagOfWords _textBagOfWords;
        private Codification _outputCodeBook;
        private NaiveBayesLearning _bayesLearning;//index 1
        private MulticlassSupportVectorLearning<Linear> _learner3;
        private MulticlassSupportVectorLearning<Linear> _multiClassLearning; //index 2     

        #region Inicialization
        
        private int[] CreateCodification(string[] folders)
        {
            _outputCodeBook = new Codification(Constants.ColumnFolders, folders);
            return _outputCodeBook.Transform(Constants.ColumnFolders, folders);
        }

        #region Learners

        private void CreateBayesLearner()
        {
            _bayesLearning = new NaiveBayesLearning();
            _bayesLearning.Options.InnerOption.Regularization = 1e-5;
        }

        private void CreateMulticlassLearner()
        {
            _learner3 = new MulticlassSupportVectorLearning<Linear>()
            {
                // Configure the learning algorithm to use SMO to train the
                //  underlying SVMs in each of the binary class subproblems.
                Learner = (param) => new SequentialMinimalOptimization<Linear>()
                {
                    // Estimate a suitable guess for the Gaussian kernel's parameters.
                    // This estimate can serve as a starting point for a grid search.

                }
            };
            _multiClassLearning = new MulticlassSupportVectorLearning<Linear>();
        }

        #endregion

        #region BagsOfWords
        private double[][] CreateInputBagOfWords(string[][] inputs)
        {
            _inputBagOfWords = new BagOfWords()
            {
                MaximumOccurance = 1
            };
            _inputBagOfWords.Learn(inputs);
            return _inputBagOfWords.Transform(inputs);
        }

        private double[][] CreateSubjectBagOfWords(string[][] inputs)
        {
            _subjectBagOfWords = new BagOfWords()
            {
                MaximumOccurance = 1
            };
            _subjectBagOfWords.Learn(inputs);
            return _subjectBagOfWords.Transform(inputs);
        }

        private double[][] CreateTextBagOfWords(string[][] inputs)
        {
            _textBagOfWords = new BagOfWords()
            {
                MaximumOccurance = 1
            };
            _textBagOfWords.Learn(inputs);
            return _textBagOfWords.Transform(inputs);
        }
        #endregion

        #endregion

        private string DecodeFolder(int codedFolder)
        {
            return _outputCodeBook?.Revert(new[] { codedFolder })[0];
        }

        private double[] ConvertInputs(string[] input, string[] subject, string[] text)
        {
            var rInput = _inputBagOfWords.Transform(input);
            var rSubject = _subjectBagOfWords.Transform(subject);
            var rText = _textBagOfWords.Transform(text);
            return AccordHelpers.CombineInput(rInput, rSubject, rText);
        }

        private void LearnMulticlassMachine(double[][] inputs, int[] outputs)
        {
            var machine = _learner3.Learn(inputs, outputs);

            _multiClassLearning = new MulticlassSupportVectorLearning<Linear>()
            {
                Model = machine, 
                Learner = (param) => new ProbabilisticOutputCalibration<Linear>()
                {
                    Model = param.Model 
                }
            };

            _multiClassLearning.ParallelOptions.MaxDegreeOfParallelism = 1;
            _multiClassLearning.Learn(inputs, outputs);
        }

        private void LearnBayesMachine(double[][] inputs, int[] outputs)
        {
            var intArray = inputs.Select(x => x.Select(i => (int)i).ToArray()).ToArray();
            _bayesLearning.Learn(intArray, outputs);
        }

        public Dictionary<string, double> DecideFolder(string[] input, string[] subject, string[] text)
        {
            var resultScores = new Dictionary<string,double>();
            var convertedInputs = ConvertInputs(input, subject, text);
            var objArray = convertedInputs.Select(v => (int)v).ToArray();
            var scores = _bayesLearning.Model.Scores(objArray);
            for (int i = 0; i < scores.Length; i++)
            {
                resultScores.Add(DecodeFolder(i), scores[i]);
            }

            return resultScores.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void CreateModel(string[][] inputs, string[][] subjects, string[][] texts, string[] results)
        {
            var convertedInputs = CreateInputBagOfWords(inputs);
            var convertedSubjects = CreateSubjectBagOfWords(subjects);
            var convertedTexts = CreateTextBagOfWords(texts);
            var result = new List<double[]>();
            for (var i = 0; i < convertedInputs.Length; i++)
            {
                result.Add(AccordHelpers.CombineInput(convertedInputs[i], convertedSubjects[i], convertedTexts[i]));
            }
            var convertedOutputs = CreateCodification(results);
            CreateBayesLearner();
            LearnBayesMachine(result.ToArray(), convertedOutputs);
        }

        public void Load(string storeName)
        {
            CreateBayesLearner();
            _bayesLearning.Model = StorageHelpers.LoadItem<NaiveBayes>(Constants.ParamModel,storeName);
            _outputCodeBook = StorageHelpers.LoadItem<Codification>(Constants.ParamCodeBook, storeName);
            _inputBagOfWords = StorageHelpers.LoadItem<BagOfWords>(Constants.ParamInputBoW, storeName);
            _subjectBagOfWords = StorageHelpers.LoadItem<BagOfWords>(Constants.ParamSubjectBoW, storeName);
            _textBagOfWords = StorageHelpers.LoadItem<BagOfWords>(Constants.ParamTextBoW, storeName);
        }

        public void Save(string storeName)
        {
            StorageHelpers.SaveItem(_bayesLearning.Model, Constants.ParamModel, storeName);
            StorageHelpers.SaveItem(_inputBagOfWords, Constants.ParamInputBoW, storeName);
            StorageHelpers.SaveItem(_subjectBagOfWords, Constants.ParamSubjectBoW, storeName);
            StorageHelpers.SaveItem(_textBagOfWords, Constants.ParamTextBoW, storeName);
            StorageHelpers.SaveItem(_outputCodeBook, Constants.ParamCodeBook, storeName);
        }

        public bool IsReady()
        {
            return _inputBagOfWords != null && _outputCodeBook != null && _subjectBagOfWords != null && _textBagOfWords != null && _bayesLearning.Model != null;
        }
    }
}
